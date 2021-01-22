using AutoMapper;
using School.Api.Resources;
using School.Core.DTOes;
using School.Core.Models;

namespace School.Api.Mapping
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<StudentDto, StudentResource>();
            CreateMap<FullStudentDto, StudentResource>();
            CreateMap<SaveStudentResource, StudentDto>();

            CreateMap<Group, GroupResource>();
            CreateMap<GroupDto, GroupResource>();
            CreateMap<SaveGroupResource, Group>();
        }
    }
}
