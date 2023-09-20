using Coffee.Domain.Models.Product.PersonalizedCoffee.Contracts;

namespace Coffee.Domain.Models.Product.PersonalizedCoffee;

public class PersonalizedCoffee : Model
{
    public PersonalizedCoffee()
    {

    }
    public PersonalizedCoffee(Guid customerId, Guid coffeId, string description, decimal priceCoffee)
    {
        CustomerId = customerId;
        CoffeId = coffeId;
        DescriptionCoffe = description;
        PriceCoffe = priceCoffee;
        TotalPrice = PriceCoffe;
        QuantityIngredient = 0;

        // Design by contracts
        AddNotifications(
            new CreatePersonalizedCoffeeContract(this)
        );
    }

    public Guid CustomerId { get; }
    public Guid CoffeId { get; private set; }
    public string DescriptionCoffe { get; private set; } = "";
    public decimal PriceCoffe { get; private set; }
    public int QuantityIngredient { get; private set; }
    public decimal TotalPrice { get; private set; }

    public ICollection<Ingredient.Ingredient> Ingredients { get; private set; } = new List<Ingredient.Ingredient>();

    public void AddIngredient(Ingredient.Ingredient ingredient)
    {
        Ingredients.Add(ingredient);
        UpdateQuantityAndTotalPrice(Ingredients);
    }

    public void RemoveIngredient(Ingredient.Ingredient ingredient)
    {
        Ingredients.Remove(ingredient);
        UpdateQuantityAndTotalPrice(Ingredients);
    }

    public void Update(Guid coffeId, string description, decimal priceCoffee)
    {
        CoffeId = coffeId;
        DescriptionCoffe = description;
        PriceCoffe = priceCoffee;
        UpdateQuantityAndTotalPrice(Ingredients);
    }

    private void UpdateQuantityAndTotalPrice(ICollection<Ingredient.Ingredient> ingredients)
    {
        QuantityIngredient = ingredients.Count;

        // sum price coffee and price ingredients
        TotalPrice = PriceCoffe;
        foreach (var ingredient in ingredients)
            TotalPrice = TotalPrice + ingredient.Price;
    }
}