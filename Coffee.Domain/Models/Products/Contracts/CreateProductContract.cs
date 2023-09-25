using Flunt.Validations;

namespace Coffee.Domain.Models.Product.Contracts;

public class CreateProductContract : Contract<Product>
{
    public CreateProductContract(Product product)
    {
        Requires()
                .IsGreaterOrEqualsThan(product.Description.Replace(" ", ""), 3, product.Description, "O nome requer no minimo 3 letras")
                .IsLowerOrEqualsThan(product.Description, 40, product.Description, "O nome deve conter no maximo 40 caracteres")
                .IsGreaterThan(product.UnitPrice, 0, product.UnitPrice.ToString(), "O valor do produto deve ser maior do que zero");
    }
}
