namespace Serilog_Structured_Logging;


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

