namespace School.Api.Resources.StudentResources
{
    public record StudentResource : SaveStudentResource
    {
        public int Id { get; init; }       
    }
}
