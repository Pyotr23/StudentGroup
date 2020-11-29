using StudentGroup.Infrastracture.Data.Models.Database;
using StudentGroup.Infrastracture.Data.Models.Filtration;
using System.Linq;

namespace StudentGroup.Infrastracture.Data.Filters
{
    public class GroupFilter
    {
        private readonly GroupFilteringParameters _filter;

        public IQueryable<Group> Query { get; private set; }

        public GroupFilter(IQueryable<Group> groups, GroupFilteringParameters filteringParameters)
        {
            Query = groups;
            _filter = filteringParameters;
        }

        public IQueryable<Group> ApplyFilter()
        {
            if (!string.IsNullOrEmpty(_filter.Name))
                Query = Query.Where(g => g.Name == _filter.Name);            

            return Query;
        }
    }

    
}
