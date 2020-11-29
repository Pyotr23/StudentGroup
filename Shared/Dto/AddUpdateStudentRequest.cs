using StudentGroup.Infrastracture.Data.Models.Database;
using System.ComponentModel.DataAnnotations;

namespace StudentGroup.Infrastracture.Shared.Dto
{
    /// <summary>
    ///     Вид сущности "Студент" в запросах на добавление и изменение.
    /// </summary>
    public class AddUpdateStudentRequest
    {
        /// <summary> Пол </summary>  
        [Required]
        public string Sex { get; set; }

        /// <summary> Фамилия </summary>
        [Required]
        [MaxLength(40)]
        public string Surname { get; set; }

        /// <summary> Имя </summary>
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        /// <summary> Отчество </summary>
        [MaxLength(60)]
        public string MiddleName { get; set; }

        /// <summary> Прозвище </summary>
        [MinLength(6)]
        [MaxLength(16)]
        public string Nickname { get; set; }   
        
        /// <summary>
        ///     Обновить студента параметрами из запроса.
        /// </summary>
        /// <param name="student"></param>
        public void UpdateStudent(Student student)
        {
            student.Sex = Sex;
            student.Surname = Surname;
            student.Name = Name;
            student.MiddleName = MiddleName;
            student.Nickname = Nickname;
        }
    }
}
