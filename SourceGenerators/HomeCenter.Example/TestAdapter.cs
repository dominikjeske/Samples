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
    [Proxy]
    public class TestAdapter : Adapter
    {
        public TestAdapter(IValueConverter valueConverter, IList<string> list)
        {
        }

        protected Task<string> Handle(CapabilitiesQuery message)
        {
            return Task.FromResult("xxx");
        }

        protected string HandleSupportedState(SupportedStatesQuery message)
        {
            return "xxx";
        }

        protected Task<string> HandleState(StateQuery message)
        {
            return Task.FromResult("xxx");
        }

        protected Task HandleState(AdjustPowerLevelCommand command)
        {
            return Task.CompletedTask;
        }

        protected void HandleState(ModeSetCommand command)
        {
        }

        protected Task OnSystemStarted(SystemStartedEvent command)
        {
            return Task.CompletedTask;
        }

        protected void OnMotion(MotionEvent command)
        {
        }

        [Subscribe(true)]
        protected Task<string> Handle(TcpQuery tcpCommand)
        {
            return Task.FromResult("");
        }

        [Subscribe]
        protected Task Handle(TcpCommand tcpCommand)
        {
            return Task.CompletedTask;
        }
    }
}