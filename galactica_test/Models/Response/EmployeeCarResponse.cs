namespace galactica_test.Models.Response
{
    /// <summary>
    /// Модель данных о машине
    /// </summary>
    public class EmployeeCarResponse
    {
        public EmployeeCarResponse(string[] licensePlate, EmployeeResponse employee)
        {
            LicensePlate = licensePlate;
            Employee = employee;
        }

        /// <summary>
        /// Госномер
        /// </summary>
        public string[] LicensePlate { get; set; }
        
        /// <summary>
        /// Информация о работнике
        /// </summary>
        public EmployeeResponse Employee { get; set; }
    }
}
