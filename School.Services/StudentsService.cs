using AutoMapper;
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
    public class StudentsService : IStudentsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStudentRepository _students;
        private readonly IMapper _mapper;

        public StudentsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _students = unitOfWork.Students;
            _mapper = mapper;
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

        public async Task<IEnumerable<StudentDto>> GetAllWithGroupNames(StudentFilterParameters filterParameters)
        {
            var students = await _students.GetStudentsWithGroupNameAsync(filterParameters);

            var studentDtoes = students
                .GroupBy(s => s.Student)
                .Select(grouping => _mapper.Map<StudentDto>(grouping));

            if (filterParameters.PageSize != 0)
                studentDtoes = studentDtoes.Take(filterParameters.PageSize);

            return studentDtoes.ToList();
        }

        public async Task<StudentDto> GetWithGroupNames(int id)
        {
            var students = await _students.GetStudentWithGroupNameAsync(id);
            if (students == null)
                return null;
            return students
                .GroupBy(s => s.Student)
                .Select(grouping => _mapper.Map<StudentDto>(grouping))
                .FirstOrDefault();
        }

        public async Task<Student> GetStudentById(int id)
        {
            return await _students.GetByIdAsync(id);
        }

        public async Task UpdateStudent(Student studentToBeUpdated, Student student)
        {
            _mapper.Map(student, studentToBeUpdated);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> IsUniqueNickname(string nickname)
        {
            return await _students.IsUniqueNickname(nickname);
        }

        public async Task<bool> IsUniqueNickname(string nickname, int id)
        {
            var studentId = await _students.GetIdByNickname(nickname);
            return studentId == null || studentId == id;
        }
    }
}
