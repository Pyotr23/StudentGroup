﻿using School.Core.Filtration.Parameters;
using School.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Core.Repositories
{
    public interface IStudentRepository : IBaseRepository<Student>
    {
        Task<Student> GetByIdAsync(int id);
        Task<int?> GetIdByNickname(string nickname);
        Task<IEnumerable<StudentWithGroupName>> GetStudentsWithGroupNameAsync(StudentFilterParameters filterParameters);
        Task<IEnumerable<StudentWithGroupName>> GetStudentWithGroupNameAsync(int id);
        Task<bool> IsUniqueNickname(string nickname);
    }
}
