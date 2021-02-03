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

        public async Task<StudentDto> CreateStudentAsync(StudentDto newStudentDto)
        {
            var newStudent = _mapper.Map<Student>(newStudentDto);
            await _students.AddAsync(newStudent);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<StudentDto>(newStudent);
        }

        public async Task DeleteStudentAsync(StudentDto studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);
            _students.Remove(student);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<FullStudentDto>> GetStudentsWithGroupNamesAsync(StudentFilterParameters filterParameters)
        {
            var students = await _students.GetStudentsWithGroupNamesAsync(filterParameters);

            //var studentDtoes = students
            //    .GroupBy(s => s.Student)
            //    .Select(grouping => _mapper.Map<FullStudentDto>(grouping));
            var studentDtoes = students
                .Select(s => new FullStudentDto
                {
                    Id = s.Id,
                    Sex = s.Sex,
                    Name = s.Name,
                    LastName = s.LastName,
                    MiddleName = s.MiddleName,
                    Nickname = s.Nickname,
                    GroupNamesToString = s.Groups == null
                        ? string.Empty
                        : string.Join(", ", s.Groups.Select(g => g.Name))
                });

            if (filterParameters.PageSize != 0)
                studentDtoes = studentDtoes.Take(filterParameters.PageSize);

            return studentDtoes.ToList();
        }         

        public async Task<StudentDto> GetStudentByIdAsync(int id)
        {
            var student = await _students.GetByIdAsync(id);
            return _mapper.Map<StudentDto>(student);
        }

        public async Task UpdateStudentAsync(StudentDto studentDtoForUpdate, StudentDto studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);
            var studentForUpdate = _mapper.Map<Student>(studentDtoForUpdate);
            _students.Attach(studentForUpdate);            
            _mapper.Map(student, studentForUpdate);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> IsUniqueNicknameAsync(string nickname)
        {
            return await _students.IsUniqueNickname(nickname);
        }

        public async Task<bool> IsUniqueNicknameAsync(string nickname, int id)
        {
            var studentId = await _students.GetIdByNicknameAsync(nickname);
            return studentId == null || studentId == id;
        }

        private async Task<Student> GetStudent(int id)
        {
            return await _students.GetByIdAsync(id);
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
            var students = await _students.GetAllAsync();
            return students
                .Select(s => _mapper.Map<StudentDto>(s))
                .ToList();
        }

        public Task<FullStudentDto> GetWithGroupNamesAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
