using Coffee.Domain.Commands.OrderCommands;
using Coffee.Domain.Models.Payments;

namespace Coffee.Domain.Commands.PaymentCommands;

public class PaymentCommandResult
{
    public PaymentCommandResult(Payment payment)
    {
        PaymentId = payment.Id.ToString();
        CustomerId = payment.CustomerId.ToString();
        Type = payment.Type.ToString();
        Price = payment.Price.ToString();
        Date = payment.Date.ToString();
        OrderId = payment.OrderId.ToString();
        Order = new OrderCommandResult(payment.Order);
    }

    public string PaymentId { get; set; } = string.Empty;
    public string CustomerId { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Price { get; set; } = string.Empty;
    public string Date { get; set; } = string.Empty;
    public string OrderId { get; set; } = string.Empty;
    public OrderCommandResult Order { get; set; }
}