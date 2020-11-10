using HomeCenter.Abstractions;

namespace HomeCenter.Messages.Commands.Device
{
    public class CalibrateCommand : Command
    {
        public static CalibrateCommand Default = new CalibrateCommand();
    }
}