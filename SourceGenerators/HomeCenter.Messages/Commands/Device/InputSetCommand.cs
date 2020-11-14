using HomeCenter.Abstractions;

namespace HomeCenter.Messages.Commands.Device
{
    public class InputSetCommand : Command
    {
        public string InputSource
        {
            get => this.AsString(MessageProperties.InputSource);
            set => this.SetProperty(MessageProperties.InputSource, value);
        }

        public static InputSetCommand Create(string input)
        {
            var command = new InputSetCommand
            {
                InputSource = input
            };
            return command;
        }
    }
}