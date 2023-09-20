using Coffee.Domain.Models.Product.PersonalizedCoffee;
using Newtonsoft.Json;

namespace Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands;

public class PersonalizedCoffeeCommandResult
{
    public PersonalizedCoffeeCommandResult(PersonalizedCoffee personalizedCoffee)
    {
        CustomerId = personalizedCoffee.CustomerId.ToString();
        DescriptionCoffe = personalizedCoffee.DescriptionCoffe;
        PriceCoffe = personalizedCoffee.PriceCoffe.ToString();
        QuantityIngredient = personalizedCoffee.QuantityIngredient.ToString();
        TotalPrice = personalizedCoffee.TotalPrice.ToString();

        Ingredients = JsonConvert.SerializeObject(personalizedCoffee.Ingredients);
        //        var ingredients = personalizedCoffee.Ingredients.ToArray().ToString();
        //        if (ingredients is not null)
        //            Ingredients = ingredients;
    }

    public string CustomerId { get; } = "";
    public string CoffeId { get; } = "";
    public string DescriptionCoffe { get; private set; } = "";
    public string PriceCoffe { get; private set; } = "";
    public string QuantityIngredient { get; private set; } = "";
    public string TotalPrice { get; private set; } = "";
    public string Ingredients { get; private set; } = "";
}