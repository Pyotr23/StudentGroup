using System.ComponentModel.DataAnnotations;

namespace School.Api.Resources.StudentResources
{
    public record FullStudentResource : SaveStudentResource
    {
        [Required]
        public int Id { get; init; }
        public string GroupNamesToString { get; init; }
    }
}
