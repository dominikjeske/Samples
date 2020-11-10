using HomeCenter.Abstractions;
using HomeCenter.Actors.Core;
using HomeCenter.Messages.Commands.Device;
using HomeCenter.Messages.Commands.Service;
using HomeCenter.Messages.Events.Device;
using HomeCenter.Messages.Events.Service;
using HomeCenter.Messages.Queries.Device;
using HomeCenter.Messages.Queries.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeCenter.SourceGenerators.Tests
{
    [GeneratedCode]
    public sealed class TestAdapterProxy : TestAdapter
    {
        public TestAdapterProxy(HomeCenter.Abstractions.IValueConverter valueConverter, System.Collections.Generic.IList<string> list, HomeCenter.Abstractions.IMessageBroker messageBroker, Microsoft.Extensions.Logging.ILogger logger) : base(valueConverter, list)
        {
            MessageBroker = messageBroker;
            Logger = logger;
        }

        protected async override Task ReceiveAsyncInternal(Proto.IContext context)
        {
            if (await HandleSystemMessages(context))
                return;
            var msg = FormatMessage(context.Message);
            if (msg is ActorMessage ic)
            {
                ic.Context = context;
            }

            if (msg is CapabilitiesQuery query_0)
            {
                var result = await Handle(query_0);
                context.Respond(result);
                return;
            }
            else if (msg is SupportedStatesQuery query_1)
            {
                var result = HandleSupportedState(query_1);
                context.Respond(result);
                return;
            }
            else if (msg is StateQuery query_2)
            {
                var result = await HandleState(query_2);
                context.Respond(result);
                return;
            }
            else if (msg is TcpQuery query_3)
            {
                var result = await Handle(query_3);
                context.Respond(result);
                return;
            }
            if (msg is AdjustPowerLevelCommand command_0)
            {
                await HandleState(command_0);
                return;
            }
            else if (msg is ModeSetCommand command_1)
            {
                HandleState(command_1);
                return;
            }
            else if (msg is TcpCommand command_2)
            {
                await Handle(command_2);
                return;
            }
            if (msg is SystemStartedEvent event_0)
            {
                await OnSystemStarted(event_0);
                return;
            }
            else if (msg is MotionEvent event_1)
            {
                OnMotion(event_1);
                return;
            }
            await UnhandledMessage(msg);
        }


        protected override async Task OnStarted(Proto.IContext context)
        {
            await base.OnStarted(context);

            Subscribe<TcpCommand>();
            Subscribe<TcpQuery, string>(true);
        }
    }
}