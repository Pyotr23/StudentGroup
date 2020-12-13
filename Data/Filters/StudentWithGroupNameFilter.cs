using StudentGroup.Infrastracture.Data.Models;
using StudentGroup.Infrastracture.Data.Models.Filtration;
using System.Linq;

namespace StudentGroup.Infrastracture.Data.Filters
{
    public class StudentWithGroupNameFilter : IFilter<StudentWithGroupName>
    {
        private readonly GroupFilteringParameters _filter;

        public IQueryable<StudentWithGroupName> Query { get; private set; }

        public StudentWithGroupNameFilter(IQueryable<StudentWithGroupName> studentsWithGroupName, GroupFilteringParameters filteringParameters)
        {
            Query = studentsWithGroupName;
            _filter = filteringParameters;
        }

        public IQueryable<StudentWithGroupName> ApplyFilter()
        {
            if (!string.IsNullOrEmpty(_filter.Name))
                Query = Query.Where(s => s.GroupName == _filter.Name);

            return Query;
        }
    }
}
