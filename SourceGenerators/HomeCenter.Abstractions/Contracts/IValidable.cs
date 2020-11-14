using System.Threading.Tasks;

namespace HomeCenter.Abstractions
{
    public interface IValidable
    {
        bool IsInverted { get; }
        Task<bool> Validate();
    }
}