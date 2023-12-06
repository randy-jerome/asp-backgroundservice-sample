namespace application.Processors;

public interface IProcessor
{
    static EventType EventType { get; }
    Task ProcessAsync();
}
