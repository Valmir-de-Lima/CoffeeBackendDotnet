using Coffee.Domain.Models.Orders.Contracts;
using Coffee.Domain.Models.Orders.Items;
using Coffee.Domain.Models.Payments;

namespace Coffee.Domain.Models.Orders;

public class Order : Model
{
    public Order()
    {

    }
    public Order(Guid customerId)
    {
        CustomerId = customerId;
        // Design by contracts
        AddNotifications(
            new CreateOrderContract(this)
        );
    }
    public Guid CustomerId { get; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Adress { get; set; } = string.Empty;
    public IList<Item> Items { get; set; } = new List<Item>();
    public Guid PaymentId { get; set; }
    public Payment Payment { get; set; } = new();


    public bool SelectedProduct(Item item)
    {
        return Items.Contains(item);
    }

    public Item? SelectedProduct(Guid id)
    {
        return Items.FirstOrDefault(x => x.ItemId == id);
    }
}