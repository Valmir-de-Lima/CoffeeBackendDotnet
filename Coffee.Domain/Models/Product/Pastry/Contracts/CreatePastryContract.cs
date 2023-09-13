using Flunt.Validations;

namespace Coffee.Domain.Models.Product.Pastry.Contracts;

public class CreatePastryContract : Contract<Pastry>
{
    public CreatePastryContract(Pastry pastry)
    {
        Requires()
                .IsGreaterOrEqualsThan(pastry.Description.Replace(" ", ""), 3, pastry.Description, "O nome requer no minimo 3 letras")
                .IsLowerOrEqualsThan(pastry.Description, 40, pastry.Description, "O nome deve conter no maximo 40 caracteres")
                .IsNotNullOrEmpty(pastry.Active.ToString(), pastry.Active.ToString(), "O valor de ativação é requerido")
                .IsGreaterThan(pastry.Price, 0, pastry.Price.ToString(), "O valor do acompanhamento deve ser maior do que zero");
    }
}
