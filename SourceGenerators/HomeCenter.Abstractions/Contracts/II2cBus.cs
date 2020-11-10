namespace HomeCenter.Abstractions
{
    public interface II2cBus
    {
        void Write(int address, byte[] data);

        void Read(int address, byte[] buffer);

        void WriteRead(int deviceAddress, byte[] writeBuffer, byte[] readBuffer);
    }
}