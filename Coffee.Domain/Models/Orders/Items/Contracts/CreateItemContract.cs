using Flunt.Validations;

namespace Coffee.Domain.Models.Orders.Items.Contracts;

public class CreateItemContract : Contract<Item>
{
    public CreateItemContract(Item item)
    {
        Requires()
                .IsGreaterOrEqualsThan(item.Description.Replace(" ", ""), 3, item.Description, "O nome requer no minimo 3 letras")
                .IsLowerOrEqualsThan(item.Description, 40, item.Description, "O nome deve conter no maximo 40 caracteres")
                .IsGreaterThan(item.UnitPrice, 0, item.UnitPrice.ToString(), "O valor do produto deve ser maior do que zero");
    }
}
