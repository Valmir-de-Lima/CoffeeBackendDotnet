using Coffee.Domain.Models.Product.Contracts;
using Coffee.Domain.Models.Product.PersonalizedCoffee.Ingredient;

namespace Coffee.Domain.Models.Product;

public class Product : Model
{
    private IList<Ingredient> _ingredients = new List<Ingredient>();
    public Product()
    {

    }
    public Product(Guid customerId, Guid productId, string description, decimal unitPrice, int quantity, bool isCoffee)
    {
        Description = description;
        CustomerId = customerId;
        ProductId = productId;
        UnitPrice = unitPrice;
        Quantity = quantity;
        TotalPrice = Quantity * UnitPrice;
        IsCoffee = isCoffee;

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
    public bool IsCoffee { get; private set; }
    public IReadOnlyCollection<Ingredient> Ingredients { get => _ingredients.ToArray(); }

    public void AddIngredients(IList<Ingredient> ingredients)
    {
        _ingredients = ingredients;
    }

    public void Update(int quantity)
    {
        Quantity = quantity;
        TotalPrice = Quantity * UnitPrice;
    }
}