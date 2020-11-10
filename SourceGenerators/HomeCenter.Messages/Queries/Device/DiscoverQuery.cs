using HomeCenter.Abstractions;
using System.Linq;

namespace HomeCenter.Messages.Queries.Device
{
    public class DiscoverQuery : Query
    {
        public static DiscoverQuery Default = new DiscoverQuery();

        public static DiscoverQuery CreateQuery(BaseObject parent)
        {
            var query = new DiscoverQuery();
            //TODO check this
            foreach (var property in parent.GetProperties().Where(p => p.Value != null))
            {
                query[property.Key] = property.Value;
            }
            return query;
        }
    }
}