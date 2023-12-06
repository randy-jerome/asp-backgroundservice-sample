namespace application.Processors;

public class ProcessorTwo : IProcessor
{
    private readonly ILogger<ProcessorTwo> logger;

    public ProcessorTwo(ILogger<ProcessorTwo> logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public static EventType EventType => EventType.Two;

    public async Task ProcessAsync()
    {
        await Task.Run(() => logger.LogInformation("Process called for ProcessorTwo"));
    }
}
