using AutoMapper;
using School.Core.DTOes;
using School.Core.Models;
using System.Linq;

namespace School.Services.Mapping
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            CreateMap<Student, StudentDto>();

            CreateMap<IGrouping<Student, StudentWithGroupName>, StudentDto>()
                .ForMember(dest => dest.GroupNamesToString,
                    opt => opt.MapFrom(src => string.Join(", ", src.Select(s => s.GroupName))))
                .IncludeMembers(src => src.Key);

            CreateMap<Student, Student>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Group, Group>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<GroupWithStudentId, GroupDto>();
        }        
    }
}
