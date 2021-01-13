using AutoMapper;
using School.Core;
using School.Core.Models;
using School.Core.Repositories;
using School.Core.Services;
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

        public async Task<Group> CreateGroup(Group newGroup)
        {
            await _groups.AddAsync(newGroup);
            await _unitOfWork.CommitAsync();
            return newGroup;
        }

        public async Task DeleteGroup(Group group)
        {
            _groups.Remove(group);
            await _unitOfWork.CommitAsync();
        }

        public async Task<Group> GetGroupById(int id)
        {
            return await _groups.GetByIdAsync(id);
        }

        public async Task UpdateGroup(Group groupToBeUpdated, Group group)
        {
            _mapper.Map(group, groupToBeUpdated);
            await _unitOfWork.CommitAsync();
        }
    }
}
