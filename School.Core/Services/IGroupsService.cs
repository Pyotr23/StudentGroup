using School.Core.DTOes;
using School.Core.Filtration.Parameters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Core.Services
{
    public interface IGroupsService
    {
        Task<GroupDto> CreateGroupAsync(GroupDto newGroupDto);
        Task DeleteGroupAsync(GroupDto group);
        Task<IEnumerable<FullGroupDto>> GetAllAsync(GroupFilterParameters filterParameters);
        Task<GroupDto> GetGroupByIdAsync(int id);
        Task UpdateGroupAsync(GroupDto groupToBeUpdated, GroupDto group);
    }
}
