using Flunt.Validations;

namespace Coffee.Domain.Models.Orders.Contracts;

public class CreateOrderContract : Contract<Order>
{
    public CreateOrderContract(Order order)
    {
        Requires()
                .IsNotNullOrEmpty(order.CustomerId.ToString(), order.CustomerId.ToString(), "Identificação requerida")
                .Matches(order.CustomerId.ToString(), "^(?:\\{{0,1}(?:[0-9a-fA-F]){8}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){12}\\}{0,1})$", "id", "Valor inválido");
    }
}
