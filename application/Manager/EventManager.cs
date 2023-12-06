namespace application.Manager;

public class EventManager : IEventManager
{
    public event EventHandler<EventType>? ProcessEvent;

    public void RaiseEvent(EventType eventType)
    {
        //Here is where you could add a service bus message that could be sent out 
        //and picked up by an azure function or some other process to process the request.
        ProcessEvent?.Invoke(this, eventType);
    }
}
