namespace Event;
public class PaymentProcessed
{
    public Guid OrderId { get; set; }
    public bool Success { get; set; }
}