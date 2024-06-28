using Event;
using MassTransit;

public class PaymentService : IConsumer<InventoryDeducted>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public PaymentService(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<InventoryDeducted> context)
    {
        var message = context.Message;

        if (message.Success)
        {
            // Process payment logic
            bool success = true; // or false based on logic

            await _publishEndpoint.Publish(new PaymentProcessed
            {
                OrderId = message.OrderId,
                Success = success
            });

            Console.WriteLine("Payment Processed Event Published");
        }
        else
        {
            // Handle inventory deduction failure
        }
    }
}
