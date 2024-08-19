using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace galactica_test.Db.Entities
{
    /// <summary>
    /// Работники
    /// </summary>
    [Table("EMPLOYEES")]
    public class EmployeeEntity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public long Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [Column("NAME")]
        public string Name { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Column("LAST_NAME")]
        public string LastName { get; set; }
    }
}
