using HomeCenter.Abstractions;

namespace HomeCenter.SourceGenerators.Tests
{
    [GeneratedCode]
    public partial class MessageGenerator
    {
        public async System.Threading.Tasks.Task<Event> PublishEvent(ActorMessage source, ActorMessage destination, IMessageBroker messageBroker, EventAggregator.RoutingFilter routingFilter)
        {
            if (destination.Type == "TestEvent")
            {
                var @event = new HomeCenter.SourceGenerators.Tests.TestEvent();
                @event.SetProperties(source);
                @event.SetProperties(destination);
                await messageBroker.Publish(@event, routingFilter);
                return @event;
            }
            else if (destination.Type == "TestEvent2")
            {
                var @event = new HomeCenter.SourceGenerators.Tests.TestEvent2();
                @event.SetProperties(source);
                @event.SetProperties(destination);
                await messageBroker.Publish(@event, routingFilter);
                return @event;
            }

            var ev = new Event();
            ev.SetProperties(source);
            ev.SetProperties(destination);
            await messageBroker.Publish(ev, routingFilter);
            return ev;
        }

        public Command CreateCommand(string message)
        {
            if (message == "TestCommand")
            {
                return new HomeCenter.SourceGenerators.Tests.TestCommand();
            }
            else if (message == "Test2Command")
            {
                return new HomeCenter.SourceGenerators.Tests.Test2Command();
            }

            return new Command();
        }
    }
}