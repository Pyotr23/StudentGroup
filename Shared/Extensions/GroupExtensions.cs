using StudentGroup.Infrastracture.Data.Models.Database;
using StudentGroup.Infrastracture.Shared.Dto;

namespace StudentGroup.Infrastracture.Shared.Extensions
{
    /// <summary>
    ///     Класс методов расширения сущностей, производных от сущности "Студент"
    /// </summary>
    public static class GroupExtensions
    {
        public static GroupDto ToDto(this AddUpdateGroupRequest bodyGroup)
        {
            return new GroupDto
            {                
                Name = bodyGroup.Name                
            };
        }

        public static Group ToGroup(this GroupDto groupDto)
        {
            return new Group
            {
                Id = 0,                
                Name = groupDto.Name
            };
        }
    }
}
