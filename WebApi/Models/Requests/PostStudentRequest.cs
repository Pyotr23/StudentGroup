namespace StudentGroup.Services.WebApi.Models.Requests
{
    public class PostStudentRequest
    {
        public string Sex { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Nickname { get; set; }
    }
}
