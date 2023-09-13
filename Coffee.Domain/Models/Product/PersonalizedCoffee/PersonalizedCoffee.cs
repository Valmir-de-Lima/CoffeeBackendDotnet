using Coffee.Domain.Models.Product.PersonalizedCoffee.Contracts;

namespace Coffee.Domain.Models.Product.PersonalizedCoffee;

public class PersonalizedCoffee : Model
{
    private IList<Ingredient.Ingredient> _ingredients = new List<Ingredient.Ingredient>();

    public PersonalizedCoffee()
    {

    }
    public PersonalizedCoffee(Guid customerId, Guid coffeId, string description, decimal unitPrice)
    {
        CustomerId = customerId;
        CoffeId = coffeId;
        DescriptionCoffe = description;
        PriceCoffe = unitPrice;
        TotalPrice = PriceCoffe;
        QuantityIngredient = 0;

        // Design by contracts
        AddNotifications(
            new CreatePersonalizedCoffeeContract(this)
        );
    }

    public Guid CustomerId { get; }
    public Guid CoffeId { get; }
    public string DescriptionCoffe { get; private set; } = "";
    public decimal PriceCoffe { get; private set; }
    public int QuantityIngredient { get; private set; }
    public decimal TotalPrice { get; private set; }

    public IReadOnlyCollection<Ingredient.Ingredient> Ingredients { get => _ingredients.ToArray(); }

    public void AddIngredient(Ingredient.Ingredient ingredient)
    {
        _ingredients.Add(ingredient);
        UpdateQuantityAndTotalPrice(_ingredients);
    }

    public void RemoveIngredient(Ingredient.Ingredient ingredient)
    {
        _ingredients.Remove(ingredient);
        UpdateQuantityAndTotalPrice(_ingredients);
    }

    private void UpdateQuantityAndTotalPrice(IList<Ingredient.Ingredient> ingredients)
    {
        QuantityIngredient = ingredients.Count;

        // sum price coffee and price ingredients
        TotalPrice = PriceCoffe;
        foreach (var ingredient in ingredients)
            TotalPrice = TotalPrice + ingredient.Price;
    }
}