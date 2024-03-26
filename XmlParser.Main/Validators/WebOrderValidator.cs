using XmlParser.Main.Models;

namespace XmlParser.Main.Validators;

public class WebOrderValidator : IValidator<WebOrder>
{
    public ValidationResult Validate(WebOrder model) 
    {
        var validationResult = new ValidationResult();

        if (model.Items is null or { Count: 0 })
        {
            validationResult.Errors.Add($"{nameof(WebOrder)} without any {nameof(WebOrder.Items)} is invalid");
            return validationResult;
        }

        if (model.Items!.Any(item => item.Price < 0))
        {
            validationResult.Errors.Add($"{nameof(WebOrder)} {nameof(WebOrder.Items)} with negative price are invalid");
        }

        if (model.Items!.Any(item => item.Quantity <= 0))
        {
            validationResult.Errors.Add($"{nameof(WebOrder)} {nameof(WebOrder.Items)} with 0 or negative quantity are invalid");
        }

        return validationResult;
    }
}
