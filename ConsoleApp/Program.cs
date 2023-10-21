using Microsoft.Extensions.Logging;
using Serilog.Formatting.Compact;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console(new CompactJsonFormatter())
    .CreateLogger();

using var loggerFactory = LoggerFactory.Create(builder => builder.AddSerilog(dispose: true));


var logger = loggerFactory.CreateLogger<Program>();



var segment = new Segment(new(1, 2), new(4, 6));
var length = segment.GetLength();
logger.LogInformation("The length of segment {@segment} is {length}.", segment, length);








public record Point(double X, double Y);

public record Segment(Point Start, Point End)
{
    public double GetLength()
    {
        var dx = Start.X - End.X;
        var dy = Start.Y - End.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
}