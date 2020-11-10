using HomeCenter.Abstractions;

namespace HomeCenter.Messages.Commands.Device
{
    public class SwitchPowerStateCommand : Command
    {
        public static SwitchPowerStateCommand Default = new SwitchPowerStateCommand();
    }
}