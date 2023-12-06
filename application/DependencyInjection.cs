using application.Manager;
using application.Processors;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace application;
public static partial class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IEventManager, EventManager>();
        //Using reflection find all classes that implement IProcessor and load them into the DI container
        var processorTypes = Assembly.GetExecutingAssembly().DefinedTypes
            .Where(e => e.ImplementedInterfaces.Any(i => i == typeof(IProcessor)));

        foreach (var processorType in processorTypes)
        {
            services.AddScoped(processorType);

            //TODO: add some error handling to check for things like the event type being used in more than one
            //processor.  Current implementation will throw a run time error if this happens, which might be ok
            //because the developer would see the error as soon as they run the app.
            var eventTypeMember = processorType.GetMember(nameof(IProcessor.EventType));
            var eventValue = ((PropertyInfo)eventTypeMember.First()).GetValue(processorType, null);

            //TODO: add some null checks.
            LoadedProcessors.Processors.Add((EventType)eventValue, processorType);
            Console.WriteLine($"Added processor [{processorType}]");
        }
        return services;

    }

}
