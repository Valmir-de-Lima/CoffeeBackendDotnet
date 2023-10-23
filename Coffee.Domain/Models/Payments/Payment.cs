using Coffee.Domain.Models.Payments.Contracts;
using Coffee.Domain.Models.Orders;

namespace Coffee.Domain.Models.Payments;

public class Payment : Model
{
    public Payment()
    {

    }
    public Payment(Guid orderId)
    {
        OrderId = orderId;
        // Design by contracts
        AddNotifications(
            new CreatePaymentContract(this)
        );
    }
    public Guid CustomerId { get; }
    public string Type { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime Date { get; set; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = new();
}