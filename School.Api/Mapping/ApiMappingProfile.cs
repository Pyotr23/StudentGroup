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
            CreateMap<Student, StudentResource>();
            CreateMap<StudentDto, StudentResource>();
            CreateMap<SaveStudentResource, Student>();
            CreateMap<GroupDto, GroupResource>();
        }
    }
}
