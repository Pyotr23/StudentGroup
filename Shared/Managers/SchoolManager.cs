using StudentGroup.Infrastracture.Data.Repositories;
using StudentGroup.Infrastracture.Shared.Dto;
using System;

namespace StudentGroup.Infrastracture.Shared.Managers
{
    public class SchoolManager : ISchoolManager
    {
        private readonly ISchoolRepository _schoolRepository;

        public SchoolManager(ISchoolRepository schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }

        public StudentDto PostStudent(AddStudentDto addStudentDto)
        {
            throw new NotImplementedException();
        }
    }
}
