using Microsoft.EntityFrameworkCore;
using StudentGroup.Infrastracture.Data.Models.Database;
using StudentGroup.Infrastracture.Data.Models.Filtration;
using System.Linq;

namespace StudentGroup.Infrastracture.Data.Filters
{
    public class StudentFilter : IFilter<Student>
    {
        private readonly StudentFilteringParameters _filter;

        public IQueryable<Student> Query { get; private set; }

        public StudentFilter(DbSet<Student> dbSet, StudentFilteringParameters filteringParameters)
        {
            Query = dbSet.AsQueryable();
            _filter = filteringParameters;
        }

        public void ApplyFilter()
        {
            if (!string.IsNullOrEmpty(_filter.Sex))
                Query = Query.Where(s => s.Sex == _filter.Sex);

            if (!string.IsNullOrEmpty(_filter.Surname))
                Query = Query.Where(s => s.Surname == _filter.Surname);

            if (!string.IsNullOrEmpty(_filter.Name))
                Query = Query.Where(s => s.Name == _filter.Name);

            if (!string.IsNullOrEmpty(_filter.MiddleName))
                Query = Query.Where(s => s.MiddleName == _filter.MiddleName);

            if (!string.IsNullOrEmpty(_filter.Nickname))
                Query = Query.Where(s => s.Nickname == _filter.Nickname);
        }        
    }
}
