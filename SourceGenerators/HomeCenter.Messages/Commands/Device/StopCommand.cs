using HomeCenter.Abstractions;

namespace HomeCenter.Messages.Commands.Device
{
    public class StopCommand : Command
    {
        public static StopCommand Default = new StopCommand();

        public static StopCommand Create(string context) => (StopCommand)new StopCommand().SetProperty(MessageProperties.Context, context);
    }
}