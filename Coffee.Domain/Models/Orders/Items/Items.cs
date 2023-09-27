using Coffee.Domain.Models.Orders.Items.Contracts;
using Coffee.Domain.Models.Orders.Items.Ingredients;

namespace Coffee.Domain.Models.Orders.Items;

public class Item : Model
{
    public Item()
    {

    }
    public Item(Guid orderId, Guid customerId, Guid itemId, string description, decimal unitPrice, int quantity, bool isCoffee)
    {
        OrderId = orderId;
        CustomerId = customerId;
        ItemId = itemId;
        Description = description;
        UnitPrice = unitPrice;
        Quantity = quantity;
        TotalPrice = Quantity * UnitPrice;
        IsCoffee = isCoffee;

        // Design by contracts
        AddNotifications(
            new CreateItemContract(this)
        );
    }

    public Guid OrderId { get; set; }
    public Order Order { get; set; } = new Order();
    public Guid CustomerId { get; set; }
    public Guid ItemId { get; set; }
    public string Description { get; set; } = "";
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public bool IsCoffee { get; set; }
    public IList<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

    public void AddIngredients(IList<Ingredient> ingredients)
    {
        Ingredients = ingredients;
    }

    public void Update(int quantity)
    {
        Quantity = Quantity + quantity;
        TotalPrice = Quantity * UnitPrice;
    }
}