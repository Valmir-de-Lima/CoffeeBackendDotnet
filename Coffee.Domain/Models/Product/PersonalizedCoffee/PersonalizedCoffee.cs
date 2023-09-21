using Coffee.Domain.Models.Product.PersonalizedCoffee.Contracts;
using Coffee.Domain.Models.Product.PersonalizedCoffee.IngredientsSelected;

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

    public Guid CustomerId { get; private set; }
    public Guid CoffeId { get; private set; }
    public string DescriptionCoffe { get; private set; } = "";
    public decimal PriceCoffe { get; private set; }
    public int QuantityIngredient { get; private set; }
    public decimal TotalPrice { get; private set; }

    public IList<IngredientSelected> Ingredients { get; set; } = new List<IngredientSelected>();

    public void AddIngredient(IngredientSelected ingredient)
    {
        Ingredients.Add(ingredient);
        TotalPrice = TotalPrice + ingredient.Price;
        QuantityIngredient += 1;
    }

    public void RemoveIngredient(IngredientSelected ingredient)
    {
        Ingredients.Remove(ingredient);
        TotalPrice = TotalPrice - ingredient.Price;
        QuantityIngredient -= 1;
    }

    public void Update(Guid coffeId, string description, decimal priceCoffee)
    {
        CoffeId = coffeId;
        DescriptionCoffe = description;
        PriceCoffe = priceCoffee;
        UpdateQuantityAndTotalPrice(Ingredients);
    }

    private void UpdateQuantityAndTotalPrice(IList<IngredientSelected> ingredients)
    {
        QuantityIngredient = ingredients.Count;

        // sum price coffee and price ingredients
        TotalPrice = PriceCoffe;
        foreach (var ingredient in ingredients)
            TotalPrice = TotalPrice + ingredient.Price;
    }
}