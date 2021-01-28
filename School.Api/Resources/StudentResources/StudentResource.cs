using System.ComponentModel.DataAnnotations;

namespace School.Api.Resources.StudentResources
{
    public record StudentResource : SaveStudentResource
    {
        [Required]
        public int Id { get; init; }       
    }
}
