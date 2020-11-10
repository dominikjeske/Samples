using HomeCenter.Abstractions;
using HomeCenter.Messages.Commands.Service;
using Proto;
using System.Linq;
using System.Runtime.InteropServices;

namespace HomeCenter.Messages.Queries.Service
{
    public class RegisterSerialCommand : Command
    {
        public Format[] ResultFormat { get; }
        public byte MessageType { get; }
        public int MessageSize { get; }
        public PID Actor { get; }

        public RegisterSerialCommand()
        {
        }

        public RegisterSerialCommand(PID actor, byte messageType, Format[] resultFormat)
        {
            Actor = actor;
            MessageType = messageType;
            MessageSize = resultFormat.Sum(format => Marshal.SizeOf(format.ValueType));
            ResultFormat = resultFormat;
        }
    }
}