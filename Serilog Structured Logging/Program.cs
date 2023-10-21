using Serilog;
using Serilog_Structured_Logging;

IHost host = Host.CreateDefaultBuilder(args)
.ConfigureServices(services =>
{
    services.AddHostedService<Worker>();
})
.UseSerilog((context, services, configuration) 
=> configuration
.ReadFrom.Configuration(context.Configuration)
.ReadFrom.Services(services)
.Enrich.FromLogContext()
.WriteTo.Console())
.Build();

host.Run();


public class Worker(ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var user = new User(Guid.NewGuid(), "Alex");
        logger.LogWarning("As User = {user}", user);

        var order = Order.Create(user);
        logger.LogWarning("Order Created = {@Order}", order);

        var product = Product.Create("Test");
        order.AddProduct(product);
        logger.LogWarning("Order.AddProduct {@Order}", order);

    }
}










