using System.ComponentModel.DataAnnotations;

namespace StudentGroup.Infrastracture.Shared.Dto
{
    /// <summary>
    ///     Вид сущности "Группа" в запросах на добавление и изменение.
    /// </summary>
    public class AddUpdateGroupRequest
    {
        /// <summary> Название </summary>
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
    }
}
