namespace School.Api.Resources
{
    public record GroupResource
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int StudentCount { get; init; }
    }
}
