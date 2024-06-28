namespace Event;
public class OrderShipped
{
    public Guid OrderId { get; set; }
    public string ShippingAddress { get; set; }
}