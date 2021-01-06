using School.Core;
using School.Core.DTOes;
using School.Core.Filtration.Parameters;
using School.Core.Models;
using School.Core.Repositories;
using School.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStudentRepository _students;

        public StudentService(IUnitOfWork unitOfWork, IStudentRepository studentRepository)
        {
            _unitOfWork = unitOfWork;
            _students = studentRepository;
        }

        public async Task<Student> CreateStudent(Student newStudent)
        {
            await _students.AddAsync(newStudent);
            await _unitOfWork.CommitAsync();
            return newStudent;
        }

        public async Task DeleteStudent(Student student)
        {
            _students.Remove(student);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<StudentWithGroupsDto>> GetAllWithGroups(StudentFilterParameters filterParameters)
        {
            var students = await _students.GetStudentsWithGroupNameAsync(filterParameters);
            return students
                .GroupBy(s => s.Student)
                .Select(x => new StudentWithGroupsDto
                {
                    Id = x.Key.Id,
                    Sex = x.Key.Sex,
                    Name = x.Key.Name,
                    MiddleName = x.Key.MiddleName,
                    LastName = x.Key.LastName,
                    GroupNames = string.Join(", ", x.Select(y => y.GroupName))
                })
                .Take(filterParameters.PageSize);             
        }

        public async Task<Student> GetStudentById(int id)
        {
            return await _students.GetByIdAsync(id);
        }

        public async Task UpdateStudent(Student studentToBeUpdated, Student student)
        {
            studentToBeUpdated.Name = student.Name;
            studentToBeUpdated.LastName = student.LastName;
            studentToBeUpdated.MiddleName = student.MiddleName;
            studentToBeUpdated.Nickname = student.Nickname;
            studentToBeUpdated.Sex = student.Sex;
            await _unitOfWork.CommitAsync();
        }
    }
}
