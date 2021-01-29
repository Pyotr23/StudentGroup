using System.ComponentModel.DataAnnotations;

namespace School.Api.Resources.GroupResources
{
    public record SaveGroupResource
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; init; }
    }
}
