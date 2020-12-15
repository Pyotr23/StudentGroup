using System.Linq;

namespace School.Core.Filtration.Filters
{
    public interface IFilter<T>
    {
        IQueryable<T> Query { get; }

        IQueryable<T> ApplyFilter();
    }
}