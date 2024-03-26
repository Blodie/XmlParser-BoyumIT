using Microsoft.AspNetCore.Http;

using FluentAssertions;
using NSubstitute;

using XmlParser.Main.Validators;

namespace XmlParser.Tests.Unit.ValidatorTests;
public class FileValidatorTests
{
    private readonly FileValidator _sut = new(0.5);
    private readonly IFormFile file = Substitute.For<IFormFile>();

    [Fact]
    public void Validate_ShouldBeValid_WhenFilePropertiesAreNotNullAndValid()
    {
        file.Length.Returns(1);
        file.ContentType.Returns("text/xml");

        var result = _sut.Validate(file);

        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1024 * 1024)]
    public void Validate_ShouldBeInvalid_WhenFileLengthIsZeroOrLargerThanMaximumAllowed(long length)
    {
        file.Length.Returns(length);
        file.ContentType.Returns("text/xml");

        var result = _sut.Validate(file);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle();
        result.Errors.Should().Contain($"File is empty or file size exceeds 0.5MB");
    }

    [Fact]
    public void Validate_ShouldBeInvalid_WhenFileContentTypeIsNotXml()
    {
        file.Length.Returns(1);
        file.ContentType.Returns("text/json");

        var result = _sut.Validate(file);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle();
        result.Errors.Should().Contain($"Please select an .XML file");
    }
}
