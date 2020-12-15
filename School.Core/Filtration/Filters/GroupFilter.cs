using School.Core.Filtration.Parameters;
using School.Core.Models;
using System.Linq;

namespace School.Core.Filtration.Filters
{
    public class GroupFilter : IFilter<Group>
    {
        private readonly GroupFilterParameters _filterParameters;

        public IQueryable<Group> Query { get; private set; }

        public GroupFilter(IQueryable<Group> groups, GroupFilterParameters filterParameters)
        {
            Query = groups;
            _filterParameters = filterParameters;
        }

        public IQueryable<Group> ApplyFilter()
        {
            if (!string.IsNullOrEmpty(_filterParameters.Name))
                Query = Query.Where(s => s.Name == _filterParameters.Name);
            
            return Query;
        }
    }
}
