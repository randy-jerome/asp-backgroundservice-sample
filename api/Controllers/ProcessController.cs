using application.Manager;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProcessController(IEventManager eventManager,
                               ILogger<ProcessController> logger) : ControllerBase
{
    private readonly IEventManager eventManager = eventManager ?? throw new ArgumentNullException(nameof(eventManager));
    private readonly ILogger<ProcessController> logger = logger ?? throw new ArgumentNullException(nameof(logger));

    [HttpPost]
    public void Post(EventType eventType)
    {
        eventManager.RaiseEvent(eventType);
    }

    [HttpGet]
    public IActionResult Get()
    {
        return base.Ok(
            LoadedProcessors.Processors.Select(
                e => new { eventType = e.Key, processorType = e.Value.ToString() }
                )
            );
    }
}
