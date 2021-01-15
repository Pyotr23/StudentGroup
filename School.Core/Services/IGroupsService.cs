﻿using School.Core.DTOes;
using School.Core.Models;
using System.Threading.Tasks;

namespace School.Core.Services
{
    public interface IGroupsService
    {
        Task<Group> CreateGroup(Group newGroup);
        Task DeleteGroup(Group group);
        Task<Group> GetGroupById(int id);
        Task<GroupDto> GetWithStudentCount(int id);
        Task UpdateGroup(Group groupToBeUpdated, Group group);
    }
}
