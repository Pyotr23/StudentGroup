using Microsoft.EntityFrameworkCore;
using StudentGroup.Infrastracture.Data.Models.Database;
using StudentGroup.Infrastracture.Data.Models.Filtration;
using System.Linq;

namespace StudentGroup.Infrastracture.Data.Filters
{
    public class GroupFilter : IFilter<Group>
    {
        private GroupFilteringParameters _filter;

        public IQueryable<Group> Query { get; private set; }

        public GroupFilter(DbSet<Group> dbSet, GroupFilteringParameters filteringParameters)
        {
            Query = dbSet.AsQueryable();
            _filter = filteringParameters;
        }

        public void ApplyFilter()
        {
            if (!string.IsNullOrEmpty(_filter.Name))
                Query = Query.Where(g => g.Name == _filter.Name);
        }
    }
}
