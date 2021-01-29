using System.ComponentModel.DataAnnotations;

namespace School.Api.Resources.GroupResources
{
    public record GroupResource : SaveGroupResource
    {
        [Required]
        public int Id { get; init; }
    }
}
