using Coffee.Domain.Commands.ProductCommands;
using Coffee.Domain.Models.Baskets;


namespace Coffee.Domain.Commands.BasketCommands;

public class BasketCommandResult
{
    public BasketCommandResult(Basket basket)
    {
        BasketId = basket.Id.ToString();
        CustomerId = basket.CustomerId.ToString();
        Quantity = basket.Quantity.ToString();
        Price = basket.Price.ToString();
        Products = basket.Products.Select(x => new ProductCommandResult(x)).ToList();
    }

    public string BasketId { get; set; } = "";
    public string CustomerId { get; set; } = "";
    public string Quantity { get; set; } = "";
    public string Price { get; set; } = "";
    public IList<ProductCommandResult> Products { get; private set; } = new List<ProductCommandResult>();
}