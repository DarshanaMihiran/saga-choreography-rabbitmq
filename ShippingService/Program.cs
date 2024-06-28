using MassTransit;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddMassTransit(x =>
                {
                    x.AddConsumer<ShippingService>();
                    x.UsingRabbitMq((ctx, cfg) =>
                    {
                        cfg.Host("rabbitmq://localhost");
                        cfg.ReceiveEndpoint("payment-processed-queue", e =>
                        {
                            e.ConfigureConsumer<ShippingService>(ctx);
                        });
                    });
                });

                services.AddMassTransitHostedService();
            })
            .Build();

await host.StartAsync();
Console.WriteLine("Shipping Service is running. Press any key to exit");
Console.ReadKey();
await host.StopAsync();