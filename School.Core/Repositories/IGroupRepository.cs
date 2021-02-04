using School.Core.DTOes;
using School.Core.Filtration.Parameters;
using School.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Core.Repositories
{
    public interface IGroupRepository : IRepository<Group>
    {
        Task<IEnumerable<GroupWithStudentCount>> GetGroupsAsync(GroupFilterParameters filterParameters);
    }
}
