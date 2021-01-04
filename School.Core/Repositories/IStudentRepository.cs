using School.Core.Models;
using System.Threading.Tasks;

namespace School.Core.Repositories
{
    public interface IStudentRepository : IBaseRepository<Student>
    {
        Task<Student> GetByIdAsync(int id);
    }
}
