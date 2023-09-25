using Coffee.Domain.Models.Baskets;
using Coffee.Domain.Models.Product.Contracts;
using Coffee.Domain.Models.Product.PersonalizedCoffee.Ingredients;

namespace Coffee.Domain.Models.Product;

public class Product : Model
{
    public Product()
    {

    }
    public Product(Guid basketId, Guid customerId, Guid productId, string description, decimal unitPrice, int quantity, bool isCoffee)
    {
        BasketId = basketId;
        CustomerId = customerId;
        ProductId = productId;
        Description = description;
        UnitPrice = unitPrice;
        Quantity = quantity;
        TotalPrice = Quantity * UnitPrice;
        IsCoffee = isCoffee;

        // Design by contracts
        AddNotifications(
            new CreateProductContract(this)
        );
    }

    public Guid BasketId { get; }
    public Basket Basket { get; set; } = new Basket();
    public Guid CustomerId { get; }
    public Guid ProductId { get; }
    public string Description { get; private set; } = "";
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }
    public decimal TotalPrice { get; private set; }
    public bool IsCoffee { get; private set; }
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