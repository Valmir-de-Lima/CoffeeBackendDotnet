using Coffee.Domain.Models.Basket.Contracts;

namespace Coffee.Domain.Models.Basket;

public class Basket : Model
{
    private IList<Product.Product> _products = new List<Product.Product>();

    public Basket()
    {

    }
    public Basket(Guid customerId, int quantity, decimal price)
    {
        CustomerId = customerId;
        Quantity = quantity;
        Price = price;

        // Design by contracts
        AddNotifications(
            new CreateBasketContract(this)
        );
    }
    public Guid CustomerId { get; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    public IReadOnlyCollection<Product.Product> Products { get => _products.ToArray(); }

    public void AddProduct(Product.Product product)
    {
        _products.Add(product);
        UpdateQuantityAndTotalPrice(_products);
    }

    public void RemoveIngredient(Product.Product product)
    {
        _products.Remove(product);
        UpdateQuantityAndTotalPrice(_products);
    }

    private void UpdateQuantityAndTotalPrice(IList<Product.Product> products)
    {
        Quantity = products.Count;

        foreach (var product in products)
            Price = Price + product.TotalPrice;
    }

}