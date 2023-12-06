using application.Manager;
using application.Processors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace application;
public class ProcessorBackgroundService : BackgroundService
{
    private readonly IEventManager eventManager;
    private readonly IServiceProvider serviceProvider;
    private readonly ILogger<ProcessorBackgroundService> logger;

    //Add logic in there if you want something to run on a schedule
    //currently this will only run on an event
    //alternatively there could be an outside scheduling process that 
    //calls the endpoint to trigger the event
    public ProcessorBackgroundService(
        IEventManager eventManager,
        IServiceProvider serviceProvider,
        ILogger<ProcessorBackgroundService> logger)
    {
        this.eventManager = eventManager ?? throw new ArgumentNullException(nameof(eventManager));
        this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        eventManager.ProcessEvent += async (sender, args) =>
        {
            await ProcessEventAsync(args);
        };
    }

    //TODO: add null checks
    private async Task ProcessEventAsync(EventType eventType)
    {
        this.logger.LogInformation($"ProcessEvent called for event [{eventType}]");
        //find the processor for the event type
        var serviceScope = serviceProvider.CreateScope();
        var processorType = LoadedProcessors.Processors.FirstOrDefault(e => e.Key == eventType);
        //service locate the processor
        var processor = serviceScope.ServiceProvider.GetRequiredService(processorType.Value);
        //run the process
        await ((IProcessor)processor).ProcessAsync();

    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //let the background service run until stopped
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
}
