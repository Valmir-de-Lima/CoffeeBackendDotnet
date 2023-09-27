using Coffee.Domain.Models.Baskets.Contracts;

namespace Coffee.Domain.Models.Baskets;

public class Basket : Model
{
    public Basket()
    {

    }
    public Basket(Guid customerId)
    {
        CustomerId = customerId;
        // Design by contracts
        AddNotifications(
            new CreateBasketContract(this)
        );
    }
    public Guid CustomerId { get; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public IList<Product.Product> Products { get; set; } = new List<Product.Product>();

    public bool SelectedProduct(Product.Product product)
    {
        return Products.Contains(product);
    }

    public Product.Product? SelectedProduct(Guid id)
    {
        return Products.FirstOrDefault(x => x.ProductId == id);
    }
}