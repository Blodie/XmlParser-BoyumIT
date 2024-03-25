using System.Xml.Serialization;

namespace XmlParser.Main.Models;

[XmlRoot("Item")]
public class WebOrderItem
{
	[XmlAttribute("id")]
	public string Id { get; set; } = default!;

	[XmlAttribute("description")]
	public string? Description { get; set; }

	[XmlElement("Quantity")]
	public int Quantity { get; set; }

	[XmlElement("Price")]
	public decimal Price { get; set; }
}
