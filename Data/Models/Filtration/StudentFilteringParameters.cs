namespace StudentGroup.Infrastracture.Data.Models.Filtration
{
    /// <summary>
    ///     Параметры фильтрации для таблицы "Студенты"
    /// </summary>
    public class StudentFilteringParameters
    {
        /// <summary>
        ///     Текст фильтрации по полу
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        ///     Текст фильтрации по фамилии
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        ///     Текст фильтрации по имени
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Текст фильтрации по отчеству
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        ///     Текст фильтрации по прозвищу
        /// </summary>
        public string Nickname { get; set; }
    }
}
