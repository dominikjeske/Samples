using System.Threading.Tasks;

namespace HomeCenter.Abstractions
{
    public sealed class EmptyCondition : IValidable
    {
        public static IValidable Default = new EmptyCondition();

        public bool IsInverted => false;

        public Task<bool> Validate() => Task.FromResult(true);
    }
}