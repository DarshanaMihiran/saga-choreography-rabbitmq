using Event;
using MassTransit;

public class ShippingService : IConsumer<PaymentProcessed>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public ShippingService(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<PaymentProcessed> context)
    {
        var message = context.Message;

        if (message.Success)
        {
            // Arrange shipping logic

            await _publishEndpoint.Publish(new OrderShipped
            {
                OrderId = message.OrderId,
                ShippingAddress = "Some Address"
            });

            Console.WriteLine("Order Shipped Event Published");
        }
        else
        {
            // Handle payment failure
        }
    }
}
