using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddMassTransit(x =>
                {
                    x.UsingRabbitMq((ctx, cfg) =>
                    {
                        cfg.Host("rabbitmq://localhost");
                    });
                });

                services.AddMassTransitHostedService();
                services.AddSingleton<OrderService>();
            })
            .Build();

await host.StartAsync();

var orderService = host.Services.GetRequiredService<OrderService>();
await orderService.CreateOrderAsync(Guid.NewGuid(), "customer1", 100.00m);

await host.StopAsync();