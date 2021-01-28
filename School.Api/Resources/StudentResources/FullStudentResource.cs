namespace School.Api.Resources.StudentResources
{
    public record FullStudentResource : SaveStudentResource
    {
        public int Id { get; init; }        
        public string GroupNamesToString { get; init; }
    }
}
