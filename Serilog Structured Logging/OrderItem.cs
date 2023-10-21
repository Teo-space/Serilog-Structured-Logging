namespace Serilog_Structured_Logging;

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


