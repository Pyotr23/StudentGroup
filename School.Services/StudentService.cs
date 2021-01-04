using School.Core;
using School.Core.DTOes;
using School.Core.Filtration.Parameters;
using School.Core.Models;
using School.Core.Repositories;
using School.Core.Services;
using System.Collections.Generic;
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

        public Task<IEnumerable<StudentWithGroupsDto>> GetAllWithGroups(StudentFilterParameters filter)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Student> GetStudentById(int id)
        {
            return await _students.GetByIdAsync(id);
        }

        public Task UpdateStudent(Student studentToBeUpdated, Student student)
        {
            studentToBeUpdated.Name = student.Name;

        }
    }
}
