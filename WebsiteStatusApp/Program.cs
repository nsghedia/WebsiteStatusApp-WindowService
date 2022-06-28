using Serilog;
using Serilog.Events;
using WebsiteStatusApp;

try
{
    Log.Information("Service started...");

    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .WriteTo.File(@"G:\Study\Practicals\Logs.txt")
        .CreateLogger();

    IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .UseSerilog()
    .Build();

    await host.RunAsync();

}
catch (Exception ex)
{
    Log.Fatal(ex, "There is some problem starting the service");
}
finally
{
    Log.CloseAndFlush();
}
