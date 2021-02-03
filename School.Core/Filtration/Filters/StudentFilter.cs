using School.Core.Filtration.Parameters;
using School.Core.Models;
using System.Linq;

namespace School.Core.Filtration.Filters
{
    public class StudentFilter : IFilter<Student>
    {
        private readonly StudentFilterParameters _filterParameters;

        public IQueryable<Student> Query { get; private set; }

        public StudentFilter(IQueryable<Student> students, StudentFilterParameters filterParameters)
        {
            Query = students;
            _filterParameters = filterParameters;
        }

        public IQueryable<Student> ApplyFilter()
        {
            if (!string.IsNullOrEmpty(_filterParameters.Sex))
                Query = Query.Where(s => s.Sex == _filterParameters.Sex);

            if (!string.IsNullOrEmpty(_filterParameters.LastName))
                Query = Query.Where(s => s.LastName == _filterParameters.LastName);

            if (!string.IsNullOrEmpty(_filterParameters.Name))
                Query = Query.Where(s => s.Name == _filterParameters.Name);

            if (!string.IsNullOrEmpty(_filterParameters.MiddleName))
                Query = Query.Where(s => s.MiddleName == _filterParameters.MiddleName);

            if (!string.IsNullOrEmpty(_filterParameters.Nickname))
                Query = Query.Where(s => s.Nickname == _filterParameters.Nickname);          

            return Query;
        }
    }
}
