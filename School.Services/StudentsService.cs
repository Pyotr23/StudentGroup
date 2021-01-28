﻿using AutoMapper;
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

            var studentDtoes = students
                .GroupBy(s => s.Student)
                .Select(grouping => _mapper.Map<FullStudentDto>(grouping));

            if (filterParameters.PageSize != 0)
                studentDtoes = studentDtoes.Take(filterParameters.PageSize);

            return studentDtoes.ToList();
        }

        public async Task<FullStudentDto> GetWithGroupNamesAsync(int id)
        {
            //var students = await _students.GetStudentsWithGroupNamesAsync(id);
            //if (students == null)
            //    return null;
            //return students
            //    .GroupBy(s => s.Student)
            //    .Select(grouping => _mapper.Map<FullStudentDto>(grouping))
            //    .FirstOrDefault();
            return null;
        }

        public async Task<StudentDto> GetStudentByIdAsync(int id)
        {
            var student = await _students.GetByIdAsync(id);
            return _mapper.Map<StudentDto>(student);
        }

        public async Task UpdateStudentAsync(int id, StudentDto studentDto)
        {
            var studentToBeUpdated = await GetStudent(id);
            if (studentToBeUpdated == null)
                return;

            var student = _mapper.Map<Student>(studentDto);
            _mapper.Map(student, studentToBeUpdated);
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
    }
}
