using MassTransit;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddMassTransit(x =>
                {
                    x.AddConsumer<PaymentService>();
                    x.UsingRabbitMq((ctx, cfg) =>
                    {
                        cfg.Host("rabbitmq://localhost");
                        cfg.ReceiveEndpoint("inventory-deducted-queue", e =>
                        {
                            e.ConfigureConsumer<PaymentService>(ctx);
                        });
                    });
                });

                services.AddMassTransitHostedService();
            })
            .Build();

await host.StartAsync();
Console.WriteLine("Payment Service is running. Press any key to exit");
Console.ReadKey();
await host.StopAsync();