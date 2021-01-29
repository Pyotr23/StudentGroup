namespace School.Api.Resources.GroupResources
{
    public record FullGroupResource : GroupResource
    {
        public int StudentCount { get; init; }
    }
}
