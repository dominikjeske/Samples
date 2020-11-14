using HomeCenter.Abstractions;

namespace HomeCenter.Messages.Commands.Device
{
    public class SetPowerLevelCommand : Command
    {
        public double PowerLevel
        {
            get => this.AsDouble(MessageProperties.PowerLevel);
            set => this.SetProperty(MessageProperties.PowerLevel, value);
        }

        public static SetPowerLevelCommand Create(double powerLevel)
        {
            var command = new SetPowerLevelCommand
            {
                PowerLevel = powerLevel
            };
            return command;
        }
    }
}