using HomeCenter.Abstractions;

namespace HomeCenter.Messages.Commands.Service
{
    public class I2cCommand : Command
    {
        public int Address
        {
            get => this.AsInt(nameof(Address));
            set => this.SetProperty(nameof(Address), value);
        }

        public byte[] Body
        {
            get => this.AsByteArray(nameof(Body));
            set => this.SetProperty(nameof(Body), value);
        }

        public static I2cCommand Create(int address, byte[] data) => new I2cCommand { Address = address, Body = data };
    }
}