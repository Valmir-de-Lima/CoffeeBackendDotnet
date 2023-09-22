using Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands.IngredientCommands;
using Coffee.Domain.Models.Product.PersonalizedCoffee;


namespace Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands;

public class PersonalizedCoffeeCommandResult
{
    public PersonalizedCoffeeCommandResult(PersonalizedCoffee personalizedCoffee)
    {
        PersonalizedCoffeeId = personalizedCoffee.Id.ToString();
        CustomerId = personalizedCoffee.CustomerId.ToString();
        CoffeId = personalizedCoffee.CoffeId.ToString();
        DescriptionCoffe = personalizedCoffee.DescriptionCoffe;
        PriceCoffe = personalizedCoffee.PriceCoffe.ToString();
        QuantityIngredient = personalizedCoffee.QuantityIngredient.ToString();
        TotalPrice = personalizedCoffee.TotalPrice.ToString();
        Ingredients = personalizedCoffee.Ingredients.Select(x => new IngredientCommandResult(x)).ToList();
    }


    public string PersonalizedCoffeeId { get; set; } = "";
    public string CustomerId { get; set; } = "";
    public string CoffeId { get; set; } = "";
    public string DescriptionCoffe { get; set; } = "";
    public string PriceCoffe { get; set; } = "";
    public string QuantityIngredient { get; set; } = "";
    public string TotalPrice { get; set; } = "";
    public IList<IngredientCommandResult> Ingredients { get; private set; } = new List<IngredientCommandResult>();
}