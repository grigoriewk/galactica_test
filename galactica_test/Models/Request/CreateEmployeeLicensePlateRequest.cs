namespace galactica_test.Models.Request;

/// <summary>
/// Модель запроса о привязке госномера к работнику
/// </summary>
public class CreateEmployeeLicensePlateRequest
{
    /// <summary>
    /// ID работника
    /// </summary>
    public long EmployeeId { get; set; }
    
    /// <summary>
    /// Госномер для привязки
    /// </summary>
    public string NewLicensePlate { get; set; }
}