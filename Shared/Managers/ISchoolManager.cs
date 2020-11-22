using StudentGroup.Infrastracture.Shared.Dto;

namespace StudentGroup.Infrastracture.Shared.Managers
{
    public interface ISchoolManager
    {
        StudentDto PostStudent(AddStudentDto addStudentDto);
    }
}
