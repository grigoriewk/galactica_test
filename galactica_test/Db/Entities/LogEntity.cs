using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace galactica_test.Db.Entities
{
    /// <summary>
    /// Лог
    /// </summary>
    [Table("LOG")]
    public class LogEntity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public long Id { get; set; }

        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        [Column("ERROR")]
        public string? ErrorMessage { get; set; }
    }
}
