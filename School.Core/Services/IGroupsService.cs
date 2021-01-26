using School.Core.DTOes;
using School.Core.Filtration.Parameters;
using School.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Core.Services
{
    public interface IGroupsService
    {
        Task<GroupDto> CreateGroup(GroupDto newGroupDto);
        Task DeleteGroup(Group group);
        Task<IEnumerable<FullGroupDto>> GetAll(GroupFilterParameters filterParameters);
        Task<GroupDto> GetGroupById(int id);
        Task<FullGroupDto> GetWithStudentCount(int id);
        Task UpdateGroup(Group groupToBeUpdated, Group group);
    }
}
