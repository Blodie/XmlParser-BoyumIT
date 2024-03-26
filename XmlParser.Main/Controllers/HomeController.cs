using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;
using System.Xml.Serialization;

using XmlParser.Main.Mappers;
using XmlParser.Main.Models;
using XmlParser.Main.ViewModels;

namespace XmlParser.Main.Controllers;
public class HomeController : Controller
{
    private const int MAX_FILE_SIZE_BYTES = 1 * 1024 * 1024; // 1 MB in bytes
	private readonly ILogger<HomeController> _logger;
    private readonly IMapper<WebOrder, WebOrderViewModel> _webOrderMapper;

    public HomeController(ILogger<HomeController> logger, IMapper<WebOrder, WebOrderViewModel> webOrderMapper)
    {
        _logger = logger;
        _webOrderMapper = webOrderMapper;
    }

    public IActionResult Index()
	{
		return View();
	}

    [HttpPost]
    public IActionResult Index(IFormFile file)
	{
        var webOrder = ProcessXml(file);
        if (webOrder is null)
        {
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

    private WebOrder? ProcessXml(IFormFile file)
    {
        if (file is null or { Length: 0 or > MAX_FILE_SIZE_BYTES })
        {
            return null;
        }

        try
        {
            using var stream = file.OpenReadStream();
            var serializer = new XmlSerializer(typeof(WebOrder));
            var model = (WebOrder?)serializer.Deserialize(stream);

            return model;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
