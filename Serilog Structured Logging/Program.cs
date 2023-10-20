using Serilog;


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


public record User(Guid UserId, string UserName) { }

public class Order
{
    public Guid OrderId { get; private set; }
    public Guid UserId { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public List<OrderItem> Items { get; set; } = new List<OrderItem>();

    public static Order Create(User user)
    {
        var Order = new Order();
        Order.OrderId = Guid.NewGuid();
        Order.UserId = user.UserId;
        Order.CreatedAt = DateTime.Now;

        return Order;
    }

    public OrderItem AddProduct(Product product)
    {
        OrderItem item = OrderItem.Create(this, product);
        Items.Add(item);
        return item;
    }
}

public class Product
{
    public Guid ProductId { get; private set; }
    public string Name { get; private set; }

    public static Product Create(string Name)
    {
        var product = new Product();
        product.ProductId = Guid.NewGuid();
        return product;
    }
}

public class OrderItem
{
    public Guid OrderId { get; set; }
    public Guid OrderItemId { get; set; }
    public Guid ProductId { get; private set; }

    public static OrderItem Create(Order order, Product product)
    {
        var item = new OrderItem();
        item.OrderId = Guid.NewGuid();
        item.OrderId = order.OrderId;
        item.ProductId = product.ProductId;


        return item;
    }
}


