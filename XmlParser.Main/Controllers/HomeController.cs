using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

using XmlParser.Main.Mappers;
using XmlParser.Main.Models;
using XmlParser.Main.Validators;
using XmlParser.Main.ViewModels;

namespace XmlParser.Main.Controllers;
public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;
    private readonly IMapper<WebOrder, WebOrderViewModel> _webOrderMapper;
    private readonly IValidator<IFormFile> _fileValidator;
    private readonly IValidator<WebOrder> _webOrderValidator;

    public HomeController(ILogger<HomeController> logger,
                          IMapper<WebOrder, WebOrderViewModel> webOrderMapper,
                          IValidator<IFormFile> fileValidator,
                          IValidator<WebOrder> webOrderValidator)
    {
        _logger = logger;
        _webOrderMapper = webOrderMapper;
        _fileValidator = fileValidator;
        _webOrderValidator = webOrderValidator;
    }

    public IActionResult Index()
	{
		return View();
	}

    [HttpPost]
    public IActionResult Index(IFormFile file)
	{
        if (file is null)
        {
            ViewBag.Errors = new List<string> { $"No file selected" };
            return View();
        }

        var fileValidationResult = _fileValidator.Validate(file);
        if (fileValidationResult.IsValid is false)
        {
            ViewBag.Errors = fileValidationResult.Errors;
            return View();
        }

        var webOrder = DeserializeFileToWebOrder(file);
        if (webOrder is null)
        {
            ViewBag.Errors = new List<string> { $"File content could not be deserialized as {nameof(WebOrder)} model" };
            return View();
        }

        var webOrderValidationResult = _webOrderValidator.Validate(webOrder);
        if (webOrderValidationResult.IsValid is false)
        {
            ViewBag.Errors = webOrderValidationResult.Errors;
            return View();
        }

        var webOrderViewModel = _webOrderMapper.MapToViewModel(webOrder);
		return View(webOrderViewModel);
	}

	public IActionResult Privacy()
	{
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}

    private static WebOrder? DeserializeFileToWebOrder(IFormFile file)
    {
        try
        {
            using var stream = file.OpenReadStream();
            var serializer = new XmlSerializer(typeof(WebOrder));
            var model = (WebOrder?) serializer.Deserialize(stream);

            return model;
        }
        catch (Exception ex) when (ex is XmlException or InvalidOperationException)
        {
            return null;
        }
    }
}
