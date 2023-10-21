namespace Serilog_Structured_Logging;

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
