using HomeCenter.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeCenter.SourceGenerators.Tests
{
    [Proxy]
    public class TestAdapter : Adapter
    {
        public TestAdapter(IValueConverter valueConverter, IList<string> list)
        {
        }

        protected Task<string> Handle(CapabilitiesQuery message)
        {
            return Task.FromResult("xxx");
        }
    }
}