using HomeCenter.Abstractions;

namespace HomeCenter.Messages.Commands.Device
{
    public class MuteCommand : Command
    {
        public static MuteCommand Default = new MuteCommand();
    }
}