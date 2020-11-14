using HomeCenter.Abstractions;
using HomeCenter.EventAggregator;
using System.Threading.Tasks;

namespace HomeCenter.SourceGenerators.Tests
{
    [GeneratedCode]
    public partial class MessageGenerator
    {
        public async Task<Event> PublishEvent(ActorMessage source, ActorMessage destination,
            IMessageBroker messageBroker, RoutingFilter routingFilter)
        {
            if (destination.Type == "TestEvent")
            {
                var @event = new TestEvent();
                @event.SetProperties(source);
                @event.SetProperties(destination);
                await messageBroker.Publish(@event, routingFilter);
                return @event;
            }

            if (destination.Type == "TestEvent2")
            {
                var @event = new TestEvent2();
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
                return new TestCommand();
            if (message == "Test2Command") return new Test2Command();

            return new Command();
        }
    }
}