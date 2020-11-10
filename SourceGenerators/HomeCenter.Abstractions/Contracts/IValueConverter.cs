namespace HomeCenter.Abstractions
{
    public interface IValueConverter
    {
        string Convert(string old);

        string ConvertBack(string old);
    }
}