namespace School.Api.Resources.StudentResources
{
    public record FullStudentResource : StudentResource
    {
        public string GroupNamesToString { get; init; }
    }
}
