namespace StudentGroup.Infrastracture.Data.Models.Filtration
{
    /// <summary>
    ///     Параметры фильтрации выборки из базы данных
    /// </summary>
    public class FilteringParameters
    {
        /// <summary>
        ///     Параметры фильтрации для таблицы "Студенты"
        /// </summary>
        public StudentFilteringParameters StudentFilteringParameters { get; set; }

        /// <summary>
        ///     Параметры фильтрации для таблицы "Группы"
        /// </summary>
        public GroupFilteringParameters GroupFilteringParameters { get; set; }

        /// <summary>
        ///     Максимальное количество возвращаемых записей 
        /// </summary>
        public int? PageSize { get; set; }
    }
}
