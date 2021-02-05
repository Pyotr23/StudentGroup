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
            CreateMap<Student, FullStudentDto>();
            CreateMap<Student, StudentDto>();
            CreateMap<StudentDto, Student>();

            CreateMap<Student, FullStudentDto>()
                .ForMember(dest => dest.GroupNamesToString,
                    opt => opt.MapFrom(src => string.Join(", ", src.Groups.Select(s => s.Name))));            
                
            CreateMap<Student, Student>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Group, Group>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<GroupWithStudentCount, FullGroupDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(g => g.Group.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(g => g.Group.Name));
                
            CreateMap<GroupDto, Group>();
            CreateMap<Group, GroupDto>();
        }        
    }
}
