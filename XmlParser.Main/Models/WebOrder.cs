using System.Xml.Serialization;

namespace XmlParser.Main.Models;

[XmlRoot("WebOrder")]
public class WebOrder
{
	[XmlAttribute("id")]
	public int Id { get; set; }

	[XmlElement("Customer")]
	public string? Customer { get; set; }

	[XmlIgnore]
    public DateOnly Date
	{
		get => DateOnly.TryParseExact(DateString, "yyyyMMdd", out var date) ? date : default; 
		set => DateString = value.ToString("yyyyMMdd"); 
	}

	[XmlElement("Date")]
	public string? DateString { get; set; }

	[XmlArray("Items")]
	public List<WebOrderItem>? Items { get; set; }
}
