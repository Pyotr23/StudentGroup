namespace StudentGroup.Infrastracture.Shared.Dto
{
    /// <summary>
    ///     DTO студента
    /// </summary>
    public class StudentDto
    {
        /// <summary> Пол </summary>        
        public string Sex { get; set; }

        /// <summary> Фамилия </summary>
        public string Surname { get; set; }

        /// <summary> Имя </summary>
        public string Name { get; set; }

        /// <summary> Отчество </summary>
        public string MiddleName { get; set; }

        /// <summary> Прозвище </summary>
        public string Nickname { get; set; }
    }
}
