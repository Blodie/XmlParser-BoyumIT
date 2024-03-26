using XmlParser.Main.Models;
using XmlParser.Main.ViewModels;

namespace XmlParser.Main.Mappers;

public class WebOrderMapper : IMapper<WebOrder, WebOrderViewModel>
{
    public WebOrderViewModel MapToViewModel(WebOrder model)
    {
        var totalPrice = model.Items?.Sum(item => item.Price * item.Quantity) ?? 0;
        var totalQuantity = model.Items?.Sum(item => item.Quantity) ?? 0;
        var average = totalQuantity is 0 ? 0 : totalPrice / totalQuantity;
        DateOnly.TryParseExact(model.Date, "yyyyMMdd", out var date);
        return new()
        {
            Id = model.Id,
            Customer = model.Customer,
            Date = date,
            Total = totalPrice,
            Average = average,
        };
    }
}
