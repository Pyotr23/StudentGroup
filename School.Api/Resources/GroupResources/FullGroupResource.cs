namespace School.Api.Resources.GroupResources
{
    public record FullGroupResource
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int StudentCount { get; init; }
    }
}
