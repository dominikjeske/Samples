using HomeCenter.Abstractions;

namespace HomeCenter.Messages.Commands.Device
{
    public class VolumeDownCommand : Command
    {
        public static VolumeDownCommand Default = new VolumeDownCommand();
    }
}