namespace galactica_test.Models.Request
{
    /// <summary>
    /// Модель запроса на изменение данных о госномере работника
    /// </summary>
    public class EditEmployeeCarRequest
    {
        /// <summary>
        /// ID работника
        /// </summary>
        public long EmployeeId { get; set; }
        
        /// <summary>
        /// Старый госномер
        /// </summary>
        public string OldLicensePlate { get; set; }
        
        /// <summary>
        /// Новый госномер
        /// </summary>
        public string NewLicensePlate { get; set; }
    }
}
