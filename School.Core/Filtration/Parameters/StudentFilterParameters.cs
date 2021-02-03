namespace School.Core.Filtration.Parameters
{
    public record StudentFilterParameters
    {
        public string Sex { get; init; }
        public string LastName { get; init; }
        public string Name { get; init; }
        public string MiddleName { get; init; }
        public string Nickname { get; init; }
        public string GroupName { get; init; }
        public int PageSize { get; init; }
        public int SkipCount { get; init; }
    }
}
