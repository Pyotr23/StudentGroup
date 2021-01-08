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
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStudentRepository _students;
        private readonly IMapper _mapper;

        public StudentService(IUnitOfWork unitOfWork, IMapper mapper)
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

        public async Task<IEnumerable<StudentWithGroupsDto>> GetAllWithGroupNames(StudentFilterParameters filterParameters)
        {
            var students = await _students.GetStudentsWithGroupNameAsync(filterParameters);
            return students
                .GroupBy(s => s.Student)
                .Select(x =>
                {
                    var studentDto = _mapper.Map<Student, StudentWithGroupsDto>(x.Key);
                    studentDto.GroupNames = string.Join(", ", x.Select(y => y.GroupName));
                    return studentDto;
                })
                .Take(filterParameters.PageSize)
                .ToList();             
        }

        public async Task<StudentWithGroupsDto> GetWithGroupNames(int id)
        {
            var students = await _students.GetStudentWithGroupNameAsync(id);
            if (students == null)
                return null;
            return students
                .GroupBy(s => s.Student)
                .Select(x => 
                //new StudentWithGroupsDto
                //{
                //    Id = x.Key.Id,
                //    Sex = x.Key.Sex,
                //    Name = x.Key.Name,
                //    LastName = x.Key.LastName,
                //    MiddleName = x.Key.MiddleName,
                //    Nickname = x.Key.Nickname,
                //    GroupNames = string.Join(", ", x.Select(y => y.GroupName))
                //}
                {
                    var studentDto = _mapper.Map<StudentWithGroupsDto>(x.Key);
                    studentDto.GroupNames = string.Join(", ", x.Select(y => y.GroupName));
                    return studentDto;
                }
                )
                .FirstOrDefault();
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
