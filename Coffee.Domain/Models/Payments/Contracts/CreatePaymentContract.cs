using Flunt.Validations;

namespace Coffee.Domain.Models.Payments.Contracts;

public class CreatePaymentContract : Contract<Payment>
{
    public CreatePaymentContract(Payment payment)
    {
        Requires()
                .IsNotNullOrEmpty(payment.OrderId.ToString(), payment.OrderId.ToString(), "Identificação requerida")
                .Matches(payment.OrderId.ToString(), "^(?:\\{{0,1}(?:[0-9a-fA-F]){8}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){4}-(?:[0-9a-fA-F]){12}\\}{0,1})$", "id", "Valor inválido");
    }
}
