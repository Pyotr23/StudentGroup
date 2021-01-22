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

        public async Task<StudentDto> CreateStudent(StudentDto newStudentDto)
        {
            var newStudent = _mapper.Map<Student>(newStudentDto);
            await _students.AddAsync(newStudent);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<StudentDto>(newStudent);
        }

        public async Task DeleteStudent(StudentDto studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);
            _students.Remove(student);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<FullStudentDto>> GetAllWithGroupNames(StudentFilterParameters filterParameters)
        {
            var students = await _students.GetStudentsWithGroupNameAsync(filterParameters);

            var studentDtoes = students
                .GroupBy(s => s.Student)
                .Select(grouping => _mapper.Map<FullStudentDto>(grouping));

            if (filterParameters.PageSize != 0)
                studentDtoes = studentDtoes.Take(filterParameters.PageSize);

            return studentDtoes.ToList();
        }

        public async Task<FullStudentDto> GetWithGroupNames(int id)
        {
            var students = await _students.GetStudentWithGroupNameAsync(id);
            if (students == null)
                return null;
            return students
                .GroupBy(s => s.Student)
                .Select(grouping => _mapper.Map<FullStudentDto>(grouping))
                .FirstOrDefault();
        }

        public async Task<StudentDto> GetStudentById(int id)
        {
            var student = await _students.GetByIdAsync(id);
            return _mapper.Map<StudentDto>(student);
        }

        public async Task UpdateStudent(StudentDto studentDtoToBeUpdated, StudentDto studentDto)
        {
            var studentToBeUpdated = _mapper.Map<Student>(studentDtoToBeUpdated);
            var student = _mapper.Map<Student>(studentDto);
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
