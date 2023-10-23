using Coffee.Domain.Models.Orders;
using Coffee.Domain.Models.Orders.Items;
using Coffee.Domain.Models.Payments;


namespace Coffee.Domain.Commands.OrderCommands;

public class OrderCommandResult
{
    public OrderCommandResult(Order order)
    {
        OrderId = order.Id.ToString();
        CustomerId = order.CustomerId.ToString();
        Quantity = order.Quantity.ToString();
        Price = order.Price.ToString();
        //Products = basket.Products.Select(x => new ProductCommandResult(x)).ToList();
    }

    public string OrderId { get; set; } = string.Empty;
    public string CustomerId { get; set; } = string.Empty;
    public string Quantity { get; set; } = string.Empty;
    public string Price { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Adress { get; set; } = string.Empty;
    public IList<Item> Items { get; set; } = new List<Item>();
    public string PaymentId { get; set; } = string.Empty;
    public Payment Payment { get; set; } = new();
}