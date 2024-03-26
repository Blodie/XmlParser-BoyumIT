using System.Xml.Serialization;

namespace XmlParser.Main.Models;

[XmlRoot("WebOrder")]
public class WebOrder
{
	[XmlAttribute("id")]
	public int Id { get; set; }

	[XmlElement("Customer")]
	public string? Customer { get; set; }

	[XmlElement("Date")]
	public string? Date { get; set; }

	[XmlArray("Items")]
	public List<Item>? Items { get; set; }
}
