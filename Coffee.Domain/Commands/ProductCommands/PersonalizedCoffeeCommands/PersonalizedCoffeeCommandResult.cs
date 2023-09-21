using Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands;
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
        Ingredients = personalizedCoffee.Ingredients.Select(x => new IngredientSelectedCommandResult(x)).ToList();
        Quantity = personalizedCoffee.Ingredients.Count().ToString();
        //Ingredients = JsonConvert.SerializeObject(personalizedCoffee.Ingredients.ToList());
        // var ingredients = personalizedCoffee.Ingredients.ToArray().ToString();
        // if (ingredients is not null)
        //     Ingredients = ingredients;
    }


    public string PersonalizedCoffeeId { get; set; } = "";
    public string CustomerId { get; set; } = "";
    public string CoffeId { get; set; } = "";
    public string DescriptionCoffe { get; set; } = "";
    public string PriceCoffe { get; set; } = "";
    public string QuantityIngredient { get; set; } = "";
    public string TotalPrice { get; set; } = "";
    public IList<IngredientSelectedCommandResult> Ingredients { get; private set; } = new List<IngredientSelectedCommandResult>();
    public string Quantity { get; set; } = "";

}