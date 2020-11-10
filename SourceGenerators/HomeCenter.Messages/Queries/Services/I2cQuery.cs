using HomeCenter.Abstractions;

namespace HomeCenter.Messages.Queries.Services
{
    public class I2cQuery : Query
    {
        public static I2cQuery Create(int address, byte[] InitializeWrite, int bufferSize) => new I2cQuery
        {
            Address = address,
            Initialize = InitializeWrite,
            BufferSize = bufferSize,
            LogLevel = nameof(Microsoft.Extensions.Logging.LogLevel.Trace)
        };

        public int Address
        {
            get => this.AsInt(MessageProperties.Address);
            set => this.SetProperty(MessageProperties.Address, value);
        }

        public byte[] Initialize
        {
            get => this.AsByteArray(MessageProperties.Initialize);
            set => this.SetProperty(MessageProperties.Initialize, value);
        }

        public int BufferSize
        {
            get => this.AsInt(MessageProperties.Size);
            set => this.SetProperty(MessageProperties.Size, value);
        }
    }
}