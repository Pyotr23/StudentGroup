namespace School.Api.Resources.StudentResources
{
    public record SaveStudentResource
    {
        public string Sex { get; init; }
        public string LastName { get; init; }
        public string Name { get; init; }
        public string MiddleName { get; init; }
        public string Nickname { get; init; }
    }
}
