namespace XmlParser.Main.Validators;

public class FileValidator : IValidator<IFormFile>
{
    private const int ONE_MB_IN_BYTES = 1024 * 1024;
    private readonly double _maxFileSizeBytes;

    public FileValidator(double maxFileSizeMB)
    {
        _maxFileSizeBytes = maxFileSizeMB * ONE_MB_IN_BYTES;
    }

    public ValidationResult Validate(IFormFile file)
    {
        var validationResult = new ValidationResult();

        if (file.Length is 0 || file.Length > _maxFileSizeBytes)
        {
            validationResult.Errors.Add($"File is empty or file size exceeds {_maxFileSizeBytes / ONE_MB_IN_BYTES}MB");
        }

        if (file.ContentType is not "text/xml")
        {
            validationResult.Errors.Add($"Please select an .XML file");
        }

        return validationResult;
    }
}
