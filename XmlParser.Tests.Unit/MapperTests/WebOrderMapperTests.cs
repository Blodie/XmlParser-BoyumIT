using FluentAssertions;

using XmlParser.Main.Mappers;

namespace XmlParser.Tests.Unit.MapperTests;
public class WebOrderMapperTests
{
    private readonly WebOrderMapper _sut = new();

    [Fact]
    public void MapToViewModel_ShouldMapWebOrderToWebOrderViewModel_WhenModelPropertiesAreNotNullAndValid()
    {
        var model = Helpers.GetValidWebOrderModel();

        var result = _sut.MapToViewModel(model);

        result.Id.Should().Be(42);
        result.Customer.Should().Be("John Smith");
        result.Date.Should().Be(new DateOnly(2016, 3, 27));
        result.Average.Should().Be(379.99m);
        result.Total.Should().Be(3039.92m);
    }

    [Fact]
    public void MapToViewModel_ShouldThrowDivideByZeroException_WhenModelItemsQuantitiesSumIsZero()
    {
        var model = Helpers.GetValidWebOrderModel();
        model.Items!.ForEach(item => item.Quantity = 0);

        var result = () => _sut.MapToViewModel(model);

        result.Should().Throw<DivideByZeroException>();
    }

    [Fact]
    public void MapToViewModel_ShouldThrowDivideByZeroException_WhenModelItemsCountIsZero()
    {
        var model = Helpers.GetValidWebOrderModel();
        model.Items = new();

        var result = () => _sut.MapToViewModel(model);

        result.Should().Throw<DivideByZeroException>();
    }

    [Fact]
    public void MapToViewModel_ShouldThrowException_WhenItemsIsNull()
    {
        var model = Helpers.GetValidWebOrderModel();
        model.Items = null;

        var result = () => _sut.MapToViewModel(model);

        result.Should().Throw<ArgumentNullException>();
    }
}
