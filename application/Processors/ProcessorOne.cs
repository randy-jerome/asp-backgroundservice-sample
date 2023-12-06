namespace application.Processors;

public class ProcessorOne : IProcessor
{
    private readonly ILogger<ProcessorOne> logger;

    public ProcessorOne(ILogger<ProcessorOne> logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public static EventType EventType => EventType.One;

    public async Task ProcessAsync()
    {

        await Task.Run(() => logger.LogInformation("Process called for ProcessorOne"));
    }
}
