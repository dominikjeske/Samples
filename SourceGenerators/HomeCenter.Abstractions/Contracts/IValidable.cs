using System.Threading.Tasks;

namespace HomeCenter.Abstractions
{
    public interface IValidable
    {
        Task<bool> Validate();

        bool IsInverted { get; }
    }
}