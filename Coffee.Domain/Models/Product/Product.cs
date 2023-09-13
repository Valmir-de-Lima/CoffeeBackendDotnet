using Coffee.Domain.Models.Product.Contracts;

namespace Coffee.Domain.Models.Product;

public class Product : Model
{
    public Product()
    {

    }
    public Product(Guid customerId, Guid productId, string description, decimal unitPrice, int quantity)
    {
        Description = description;
        CustomerId = customerId;
        ProductId = productId;
        UnitPrice = unitPrice;
        Quantity = quantity;
        TotalPrice = Quantity * UnitPrice;

        // Design by contracts
        AddNotifications(
            new CreateProductContract(this)
        );
    }

    public Guid CustomerId { get; }
    public Guid ProductId { get; }
    public string Description { get; private set; } = "";
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }
    public decimal TotalPrice { get; private set; }

    public void Update(int quantity)
    {
        Quantity = quantity;
        TotalPrice = Quantity * UnitPrice;
    }
}