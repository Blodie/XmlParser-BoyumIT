using System.ComponentModel.DataAnnotations;

namespace XmlParser.Main.ViewModels;

public class WebOrderViewModel
{
    public int Id { get; set; }
    public string? Customer { get; set; }
    public string? Date { get; set; }
    public string? Total { get; set; }
    public string? Average { get; set; }
}
