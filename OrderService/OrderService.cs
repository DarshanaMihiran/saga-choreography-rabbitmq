using Event;
using MassTransit;

public class OrderService
{
    private readonly IPublishEndpoint _publishEndpoint;

    public OrderService(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task CreateOrderAsync(Guid orderId, string customerId, decimal totalAmount)
    {
        // Validate and create the order
        // ...

        await _publishEndpoint.Publish(new OrderCreated
        {
            OrderId = orderId,
            CustomerId = customerId,
            TotalAmount = totalAmount
        });

        Console.WriteLine("Order Created Event Published");
    }
}
