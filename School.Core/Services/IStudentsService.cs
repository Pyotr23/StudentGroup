﻿using School.Core.DTOes;
using School.Core.Filtration.Parameters;
using School.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Core.Services
{
    public interface IStudentsService
    {
        Task<IEnumerable<FullStudentDto>> GetAllWithGroupNames(StudentFilterParameters filter);
        Task<Student> GetStudentById(int id);
        Task<StudentDto> CreateStudent(StudentDto newStudent);
        Task UpdateStudent(Student studentToBeUpdated, Student student);
        Task DeleteStudent(Student student);
        Task<FullStudentDto> GetWithGroupNames(int id);
        Task<bool> IsUniqueNickname(string nickname);
        Task<bool> IsUniqueNickname(string nickname, int id);
    }
}