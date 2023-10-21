using Serilog;
using Serilog.Sinks.Elasticsearch;
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
.WriteTo.Console()

.WriteTo.MongoDBBson("mongodb://127.0.0.1:27017/logs")

//docker run --name influxdb -p 8086:8086 influxdb:latest
// username/pass influxdb
// generate token in web interface using username/pass
//Create organization/bucket, get organization id
.WriteTo.InfluxDB(applicationName: "Quick test",
                uri: new Uri("http://127.0.0.1:8086"),
                organizationId: "5b5d545e92a59039",
                bucketName: "logs",
                token: "7Jje9xYjaV5rhqnuLYrnSVyJjHRM2kLg7Fivec8sLli2nKp3svpeOa1AleHb657MuV-Ft0jkwMElqfyjVhkkdg==")

//docker network create elastic
//docker run - d--name elasticsearch--net elastic - p 9200:9200 - p 9300:9300 - e "discovery.type=single-node" elasticsearch: 8.10.2
.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200")))



)
.Build();
host.Run();













