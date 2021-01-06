using AutoMapper;
using School.Api.Resources;
using School.Core.Models;

namespace School.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentResource>();
        }
    }
}
