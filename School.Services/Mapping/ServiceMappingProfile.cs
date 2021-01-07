using AutoMapper;
using School.Core.DTOes;
using School.Core.Models;

namespace School.Services.Mapping
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            CreateMap<Student, StudentWithGroupsDto>();
        }        
    }
}
