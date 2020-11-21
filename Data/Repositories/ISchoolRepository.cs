using StudentGroup.Infrastracture.Data.Models;

namespace StudentGroup.Infrastracture.Data.Repositories
{
    public interface ISchoolRepository
    {
        Student PostStudent(Student student);
    }
}
