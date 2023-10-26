using Coffee.Domain.Models.Orders.Items;
using Coffee.Domain.Commands.OrderCommands.ItemCommands.IngredientCommands;

namespace Coffee.Domain.Commands.OrderCommands.ItemCommands;

public class ItemCommandResult
{
    public ItemCommandResult(Item item)
    {
        OrderId = item.OrderId.ToString();
        CustomerId = item.CustomerId.ToString();
        Description = item.Description;
        UnitPrice = item.UnitPrice.ToString();
        Quantity = item.Quantity.ToString();
        TotalPrice = item.TotalPrice.ToString();
        QuantityIngredient = item.Ingredients.Count().ToString();
        Ingredients = item.Ingredients.Select(x => new IngredientCommandResult(x)).ToList();
    }


    public string OrderId { get; set; } = "";
    public string CustomerId { get; set; } = "";
    public string Description { get; set; } = "";
    public string UnitPrice { get; set; } = "";
    public string Quantity { get; set; } = "";
    public string QuantityIngredient { get; set; } = "";
    public string TotalPrice { get; set; } = "";
    public IList<IngredientCommandResult> Ingredients { get; private set; } = new List<IngredientCommandResult>();
}