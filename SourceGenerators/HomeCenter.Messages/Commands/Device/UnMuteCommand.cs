using HomeCenter.Abstractions;

namespace HomeCenter.Messages.Commands.Device
{
    public class UnmuteCommand : Command
    {
        public static UnmuteCommand Default = new UnmuteCommand();
    }
}