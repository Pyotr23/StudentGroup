namespace StudentGroup.Infrastracture.Shared.Dto
{
    /// <summary>
    ///     Студент со списком групп
    /// </summary>
    public class GetStudentsResponse
    {
        /// <summary> Идентификатор </summary>
        public int Id { get; set; }

        /// <summary> Фамилия </summary>
        public string Surname { get; set; }

        /// <summary> Имя </summary>
        public string Name { get; set; }

        /// <summary> Фамилия </summary>
        public string MiddleName { get; set; }

        /// <summary> Прозвище </summary>
        public string Nickname { get; set; }

        /// <summary> Список названий групп через точку с запятой </summary>
        public string GroupNamesString { get; set; }
    }
}
