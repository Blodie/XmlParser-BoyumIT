using XmlParser.Main.Models;
using XmlParser.Main.ViewModels;

namespace XmlParser.Main.Mappers;

public class WebOrderMapper : IMapper<WebOrder, WebOrderViewModel>
{
    public WebOrderViewModel MapToViewModel(WebOrder model)
    {
        var totalPrice = model.Items!.Sum(item => item.Price * item.Quantity);
        var totalQuantity = model.Items!.Sum(item => item.Quantity);
        DateOnly.TryParseExact(model.Date, "yyyyMMdd", out var date);
        return new()
        {
            Id = model.Id,
            Customer = model.Customer,
            Date = date,
            Total = totalPrice,
            Average = totalPrice / totalQuantity,
        };
    }
}
