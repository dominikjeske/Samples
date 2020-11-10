using HomeCenter.Abstractions;

namespace HomeCenter.Messages.Commands.Device
{
    public class AdjustPowerLevelCommand : Command
    {
        public static AdjustPowerLevelCommand Create(double delta)
        {
            var command = new AdjustPowerLevelCommand
            {
                Delta = delta
            };
            return command;
        }

        public double Delta
        {
            get => this.AsDouble(MessageProperties.Delta);
            set => this.SetProperty(MessageProperties.Delta, value);
        }
    }
}