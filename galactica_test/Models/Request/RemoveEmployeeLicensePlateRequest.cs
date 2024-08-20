namespace galactica_test.Models.Request;

/// <summary>
/// Модель запроса для отвязки госномера от работника
/// </summary>
public class RemoveEmployeeLicensePlateRequest
{
    /// <summary>
    /// ID работника
    /// </summary>
    public long EmployeeId { get; set; }
    
    /// <summary>
    /// Госномер для отвязки
    /// </summary>
    public string LicensePlateToRemove { get; set; }
}