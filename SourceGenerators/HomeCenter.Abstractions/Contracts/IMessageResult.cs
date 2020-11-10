namespace HomeCenter.Abstractions
{
    public interface IMessageResult<T, R>
    {
        bool Verify(T input, R expectedResult);
    }
}