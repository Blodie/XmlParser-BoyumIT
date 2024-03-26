using System.ComponentModel.DataAnnotations;

namespace XmlParser.Main.ViewModels;

public class WebOrderViewModel
{
    public int Id { get; set; }
    public string? Customer { get; set; }

    [DisplayFormat(DataFormatString = "{0:dd. MMMM. yyyy}")]
    public DateOnly Date { get; set; }

    [DisplayFormat(DataFormatString = "{0:#,#.000}")]
    public decimal Total { get; set; }

    [DisplayFormat(DataFormatString = "{0:#,#.000}")]
    public decimal Average { get; set; }
}
