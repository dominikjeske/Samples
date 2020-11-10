using HomeCenter.Abstractions;

namespace HomeCenter.Messages.Commands.Device
{
    public class TurnOffCommand : Command
    {
        public static TurnOffCommand Default = new TurnOffCommand();
    }
}