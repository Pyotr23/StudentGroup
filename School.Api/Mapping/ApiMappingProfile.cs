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
            CreateMap<StudentDto, FullStudentResource>();
            CreateMap<FullStudentDto, FullStudentResource>();
            CreateMap<SaveStudentResource, StudentDto>();
            CreateMap<StudentDto, StudentResource>();

            CreateMap<Group, FullGroupResource>();
            CreateMap<GroupDto, FullGroupResource>();
            CreateMap<SaveGroupResource, GroupDto>();
            CreateMap<GroupDto, GroupResource>();
        }
    }
}
