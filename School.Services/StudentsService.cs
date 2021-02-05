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
        private readonly IGroupRepository _groups;
        private readonly IMapper _mapper;

        public StudentsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _students = unitOfWork.Students;
            _groups = unitOfWork.Groups;
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
            var students = await _students.GetStudentsAsync(filterParameters);            
            var studentDtoes = students
                .Select(student => _mapper.Map<FullStudentDto>(student));

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
            return await _students.IsUniqueNicknameAsync(nickname);
        }

        public async Task AddStudentToGroupAsync(int studentId, GroupDto groupDto)
        {            
            var student = await _students.GetStudentWithGroupsByIdAsync(studentId);

            var group = _mapper.Map<Group>(groupDto);
            if (student.Groups.Contains(group))
                return;

            student.Groups.Add(group);
            await _unitOfWork.CommitAsync();
        }

        public async Task<FullStudentDto> GetFullStudentInfoAsync(int id)
        {
            var student = await _students.GetStudentWithGroupsByIdAsync(id);
            return _mapper.Map<FullStudentDto>(student);
        }

        public async Task DeleteStudentFromGroupAsync(int studentId, GroupDto groupDto)
        {
            var student = await _students.GetStudentWithGroupsByIdAsync(studentId);            

            var group = student.Groups.FirstOrDefault(g => g.Id == groupDto.Id);
            if (group == null)
                return;

            student.Groups.Remove(group);
            await _unitOfWork.CommitAsync();
        }
    }
}
