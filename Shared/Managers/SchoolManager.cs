using StudentGroup.Infrastracture.Data.Models;
using StudentGroup.Infrastracture.Data.Repositories;
using StudentGroup.Infrastracture.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentGroup.Infrastracture.Shared.Managers
{
    public class SchoolManager : ISchoolManager
    {
        private readonly ISchoolRepository _schoolRepository;

        public SchoolManager(ISchoolRepository schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }

        public async Task<IEnumerable<StudentWithGroups>> GetAllStudentsWithGroups()
        {
            return await _schoolRepository.GetStudentsWithGroupsAsync();                                    
        }

        public StudentDto PostStudent(AddStudentDto addStudentDto)
        {
            throw new NotImplementedException();
        }
    }
}
