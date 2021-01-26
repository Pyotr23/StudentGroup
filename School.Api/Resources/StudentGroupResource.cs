namespace School.Api.Resources
{
    public record StudentGroupResource
    {
        public StudentResource Student { get; init; }
        public FullGroupResource Group { get; init; }
    }
}
