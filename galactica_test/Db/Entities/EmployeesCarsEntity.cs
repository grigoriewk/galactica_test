using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace galactica_test.Db.Entities
{
    /// <summary>
    /// Машины работников
    /// </summary>
    [Table("EMPLOYEES_CARS")]
    public class EmployeesCarsEntity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор работника
        /// </summary>
        [Column("EMPLOYEE_ID")]
        public long EmployeeId { get; set; }

        /// <summary>
        /// Работник
        /// </summary>
        [ForeignKey(nameof(EmployeeId))]
        public virtual EmployeeEntity Employee { get; set; } = null!;

        /// <summary>
        /// Госномер
        /// </summary>
        [Column("LICENSE_PLATE")]
        public string LicensePlateNumber { get; set; }
    }
}
