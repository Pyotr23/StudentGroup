using School.Core.Filtration.Parameters;
using School.Core.Models;
using System.Linq;

namespace School.Data.Extensions
{
    public static class StudentExtensions
    {
        public static IQueryable<Student> FilterByGroupName(
            this IQueryable<Student> students, 
            string groupName)
        {
            return students
                .Where(student => string.IsNullOrEmpty(groupName)
                    || student
                        .Groups
                        .Any(g => g.Name == groupName));
        }

        public static IQueryable<Student> WithPagination(
            this IQueryable<Student> students,
            StudentFilterParameters filterParameters
            )
        {
            return students
                .OrderBy(s => s.Id)
                .Skip(filterParameters.SkipCount)
                .Take(filterParameters.PageSize == 0
                    ? int.MaxValue
                    : filterParameters.PageSize);
        }            
    }
}
