namespace XmlParser.Main.Validators;

public interface IValidator<T>
{
    ValidationResult Validate(T model);
}
