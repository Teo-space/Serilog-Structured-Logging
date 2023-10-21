namespace Serilog_Structured_Logging;

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