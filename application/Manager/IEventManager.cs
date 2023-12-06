namespace application.Manager;
public interface IEventManager
{
    event EventHandler<EventType> ProcessEvent;
    void RaiseEvent(EventType eventType);
}
