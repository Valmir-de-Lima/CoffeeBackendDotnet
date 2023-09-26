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

    public void AddProduct(Product.Product product)
    {
        if (!SelectedProduct(product))
        {
            Products.Add(product);
            UpdateQuantityAndTotalPrice(Products);
        }
    }

    public void RemoveProduct(Product.Product product)
    {
        if (SelectedProduct(product))
        {
            Products.Remove(product);
            UpdateQuantityAndTotalPrice(Products);
        }
    }

    public void IncreaseQuantityProduct(Product.Product product)
    {
        int index = Products.IndexOf(product);
        if (index != -1)
        {
            Product.Product selectedProduct = Products[index];
            selectedProduct.Update(1);
            UpdateQuantityAndTotalPrice(Products);
        }
    }

    public void DecreaseQuantityProduct(Product.Product product)
    {
        int index = Products.IndexOf(product);
        if (index != -1)
        {
            Product.Product selectedProduct = Products[index];
            if (selectedProduct.Quantity >= 2)
            {
                selectedProduct.Update(-1);
            }
            else
            {
                Products.Remove(product);
            }
            UpdateQuantityAndTotalPrice(Products);
        }
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