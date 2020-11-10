using HomeCenter.Abstractions;

namespace HomeCenter.Messages.Commands.Device
{
    public class SendCodeCommand : Command
    {
        public uint Code
        {
            get => this.AsUint(nameof(Code));
            set => this.SetProperty(nameof(Code), value);
        }

        public int System
        {
            get => this.AsInt(nameof(System));
            set => this.SetProperty(nameof(System), value);
        }

        public int Bits
        {
            get => this.AsInt(nameof(Bits));
            set => this.SetProperty(nameof(Bits), value);
        }

        public int Repeat
        {
            get => this.AsInt(nameof(Repeat));
            set => this.SetProperty(nameof(Repeat), value);
        }

        public static SendCodeCommand Create(uint code, int system = 7, int bits = 32, int repeat = 1) => new SendCodeCommand { Code = code, System = system, Bits = bits, Repeat = repeat };
    }
}