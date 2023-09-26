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
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    public IList<Product.Product> Products { get; set; } = new List<Product.Product>();

    public void AddProduct()
    {
        UpdateQuantityAndTotalPrice(Products);
    }

    public void RemoveProduct()
    {
        UpdateQuantityAndTotalPrice(Products);
    }

    public void IncreaseQuantityProduct()
    {
        UpdateQuantityAndTotalPrice(Products);
    }

    public void DecreaseQuantityProduct()
    {
        UpdateQuantityAndTotalPrice(Products);
    }

    public bool SelectedProduct(Product.Product product)
    {
        return Products.Contains(product);
    }

    private void UpdateQuantityAndTotalPrice(IList<Product.Product> products)
    {
        Quantity = 0;
        Price = 0;
        foreach (var product in products)
        {
            Price = Price + product.TotalPrice;
            Quantity = Quantity + product.Quantity;
        }
    }
}