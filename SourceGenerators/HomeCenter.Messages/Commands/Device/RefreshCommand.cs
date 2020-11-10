using HomeCenter.Abstractions;

namespace HomeCenter.Messages.Commands.Device
{
    public class RefreshCommand : Command
    {
        public static RefreshCommand Default = new RefreshCommand();

        public RefreshCommand()
        {
            LogLevel = nameof(Microsoft.Extensions.Logging.LogLevel.Trace);
        }
    }
}