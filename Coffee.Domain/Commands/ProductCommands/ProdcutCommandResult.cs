using Coffee.Domain.Commands.ProductCommands.PersonalizedCoffeeCommands.IngredientCommands;
using Coffee.Domain.Models.Product;


namespace Coffee.Domain.Commands.ProductCommands;

public class ProductCommandResult
{
    public ProductCommandResult(Product product)
    {
        ProductId = product.Id.ToString();
        CustomerId = product.CustomerId.ToString();
        Description = product.Description;
        UnitPrice = product.UnitPrice.ToString();
        Quantity = product.Quantity.ToString();
        TotalPrice = product.TotalPrice.ToString();
        QuantityIngredient = product.Ingredients.Count().ToString();
        Ingredients = product.Ingredients.Select(x => new IngredientCommandResult(x)).ToList();
    }


    public string ProductId { get; set; } = "";
    public string CustomerId { get; set; } = "";
    public string Description { get; set; } = "";
    public string UnitPrice { get; set; } = "";
    public string Quantity { get; set; } = "";
    public string QuantityIngredient { get; set; } = "";
    public string TotalPrice { get; set; } = "";
    public IList<IngredientCommandResult> Ingredients { get; private set; } = new List<IngredientCommandResult>();
}