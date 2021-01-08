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
            CreateMap<StudentWithGroupsDto, StudentResource>();
            CreateMap<SaveStudentResource, Student>();
        }
    }
}
