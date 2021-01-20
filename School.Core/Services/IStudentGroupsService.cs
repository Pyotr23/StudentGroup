using School.Core.Models;
using System.Threading.Tasks;

namespace School.Core.Services
{
    public interface IStudentGroupsService
    {
        Task<StudentGroup> AddStudentToGroup(StudentGroup studentGroup);
        Task<StudentGroup> GetStudentGroup(int studentId, int groupId);
    }
}
