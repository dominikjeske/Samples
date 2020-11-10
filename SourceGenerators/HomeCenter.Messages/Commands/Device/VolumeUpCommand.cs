using HomeCenter.Abstractions;

namespace HomeCenter.Messages.Commands.Device
{
    public class VolumeUpCommand : Command
    {
        public static VolumeUpCommand Default = new VolumeUpCommand();
    }
}