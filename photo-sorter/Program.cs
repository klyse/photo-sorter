using Microsoft.Extensions.Hosting;
using photo_sorter.Verbs;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

try
{
    await Host.CreateDefaultBuilder()
        .ConfigureCocona(args, new[] { typeof(DeleteVerb) })
        .UseSerilog()
        .Build()
        .RunAsync();
}
catch (Exception e)
{
    Log.Error(e, "Hosting error");
}
finally
{
    Log.CloseAndFlush();
}