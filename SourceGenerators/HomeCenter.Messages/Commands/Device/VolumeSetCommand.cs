using HomeCenter.Abstractions;

namespace HomeCenter.Messages.Commands.Device
{
    public class VolumeSetCommand : Command
    {
        public double Value
        {
            get => this.AsDouble(MessageProperties.Value);
            set => this.SetProperty(MessageProperties.Value, value);
        }

        public static VolumeSetCommand Create(double volume)
        {
            var command = new VolumeSetCommand
            {
                Value = volume
            };
            return command;
        }
    }
}