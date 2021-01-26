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
    public class GroupsService : IGroupsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupRepository _groups;
        private readonly IMapper _mapper;

        public GroupsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _groups = unitOfWork.Groups;
            _mapper = mapper;
        }

        public async Task<GroupDto> CreateGroup(GroupDto newGroupDto)
        {
            var newGroup = _mapper.Map<Group>(newGroupDto);
            await _groups.AddAsync(newGroup);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<GroupDto>(newGroup);
        }

        public async Task DeleteGroup(Group group)
        {
            _groups.Remove(group);
            await _unitOfWork.CommitAsync();
        }

        public async Task<GroupDto> GetGroupById(int id)
        {
            var group = await _groups.GetByIdAsync(id);
            return _mapper.Map<GroupDto>(group);
        }

        public async Task UpdateGroup(Group groupToBeUpdated, Group group)
        {
            _mapper.Map(group, groupToBeUpdated);
            await _unitOfWork.CommitAsync();
        }

        public async Task<FullGroupDto> GetWithStudentCount(int id)
        {             
            var group = await _groups.GetGroupWithStudentCountAsync(id);            
            return _mapper.Map<FullGroupDto>(group);
        }

        public async Task<IEnumerable<FullGroupDto>> GetAll(GroupFilterParameters filterParameters)
        {
            var groups = await _groups.GetAllGroups(filterParameters);
            return groups
                .Select(g => _mapper.Map<FullGroupDto>(g))
                .ToList();
        }
    }
}
