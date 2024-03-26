using XmlParser.Main.Models;

namespace XmlParser.Tests.Unit;
public static class Helpers
{
    public static WebOrder GetValidWebOrderModel()
    {
        return new()
        {
            Id = 42,
            Customer = "John Smith",
            Date = "20160327",
            Items =
            [
                new() { Id = "B1234", Description = "SAP Business One", Quantity = 4, Price = 133.33m },
                new() { Id = "A5678", Description = "Microsoft Office 2016", Quantity = 2, Price = 1143.00m },
                new() { Id = "A2222", Description = "MSSQL Server 2014", Quantity = 2, Price = 110.30m },
            ]
        };
    }
}
