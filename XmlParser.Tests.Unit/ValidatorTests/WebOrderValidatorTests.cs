using FluentAssertions;

using XmlParser.Main.Models;
using XmlParser.Main.Validators;

namespace XmlParser.Tests.Unit.ValidatorTests;
public class WebOrderValidatorTests
{
    private readonly WebOrderValidator _sut = new();

    [Fact]
    public void Validate_ShouldBeValid_WhenModelPropertiesAreNotNullAndValid()
    {
        var model = Helpers.GetValidWebOrderModel();

        var result = _sut.Validate(model);

        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Validate_ShouldBeInvalid_WhenModelItemsIsNullOrCountIsZero(bool isItemsNull)
    {
        var model = Helpers.GetValidWebOrderModel();
        model.Items = isItemsNull ? null : new();

        var result = _sut.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle();
        result.Errors.Should().Contain($"{nameof(WebOrder)} without any {nameof(WebOrder.Items)} is invalid");
    }

    [Fact]
    public void Validate_ShouldBeInvalid_WhenModelItemsContainsNull()
    {
        var model = Helpers.GetValidWebOrderModel();
        model.Items![0] = null;

        var result = _sut.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle();
        result.Errors.Should().Contain($"{nameof(WebOrder)} {nameof(WebOrder.Items)} containing null is invalid");
    }

    [Fact]
    public void Validate_ShouldBeInvalid_WhenModelItemsContainsNegativePrice()
    {
        var model = Helpers.GetValidWebOrderModel();
        model.Items![0].Price = -1;

        var result = _sut.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle();
        result.Errors.Should().Contain($"{nameof(WebOrder)} {nameof(WebOrder.Items)} with negative price are invalid");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validate_ShouldBeInvalid_WhenModelItemsContainsZeroOrNegativeQuantity(int quantity)
    {
        var model = Helpers.GetValidWebOrderModel();
        model.Items![0].Quantity = quantity;

        var result = _sut.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle();
        result.Errors.Should().Contain($"{nameof(WebOrder)} {nameof(WebOrder.Items)} with 0 or negative quantity are invalid");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validate_ShouldBeInvalidWithTwoErrorMessages_WhenModelItemsContainsZeroOrNegativeQuantityAndContainNegativePrice(int quantity)
    {
        var model = Helpers.GetValidWebOrderModel();
        model.Items![0].Price = -1;
        model.Items![0].Quantity = quantity;

        var result = _sut.Validate(model);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(2);
        result.Errors.Should().Contain($"{nameof(WebOrder)} {nameof(WebOrder.Items)} with negative price are invalid");
        result.Errors.Should().Contain($"{nameof(WebOrder)} {nameof(WebOrder.Items)} with 0 or negative quantity are invalid");
    }
}
