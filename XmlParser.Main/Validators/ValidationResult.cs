namespace XmlParser.Main.Validators;

public class ValidationResult
{
    public bool IsValid => Errors.Count is 0;
    public List<string> Errors { get; set; } = [];
}
