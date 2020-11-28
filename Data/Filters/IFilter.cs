using StudentGroup.Infrastracture.Data.Models.Database;
using System.Linq;

namespace StudentGroup.Infrastracture.Data.Filters
{
    public interface IFilter<T>
    {
        IQueryable<T> Query { get; }

        void ApplyFilter();
    }
}