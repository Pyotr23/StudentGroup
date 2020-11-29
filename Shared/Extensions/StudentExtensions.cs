using StudentGroup.Infrastracture.Data.Models.Database;
using StudentGroup.Infrastracture.Shared.Dto;

namespace StudentGroup.Infrastracture.Shared.Extensions
{
    /// <summary>
    ///     Класс методов расширения сущностей, производных от сущности "Студент"
    /// </summary>
    public static class StudentExtensions
    {
        public static StudentDto ToDto(this AddUpdateStudentRequest bodyStudent)
        {
            return new StudentDto
            {
                Sex = bodyStudent.Sex,
                Surname = bodyStudent.Surname,
                Name = bodyStudent.Name,
                MiddleName = bodyStudent.MiddleName,
                Nickname = bodyStudent.Nickname
            };
        }

        public static Student ToStudent(this StudentDto studentDto)
        {
            return new Student
            {
                Id = 0,
                Sex = studentDto.Sex,
                Surname = studentDto.Surname,
                Name = studentDto.Name,
                MiddleName = studentDto.MiddleName,
                Nickname = studentDto.Nickname
            };
        }
    }
}
