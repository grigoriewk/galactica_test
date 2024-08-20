namespace galactica_test.Models.Response;

/// <summary>
/// Информация о госномере
/// </summary>
public class CheckEmployeeLicensePlatesResponse
{
    /// <summary>
    /// Госномер существует в системе
    /// </summary>
    public bool IsExist { get; set; }
    
    /// <summary>
    /// Данные о работнике
    /// </summary>
    public EmployeeResponse Employee { get; set; }
    
    /// <summary>
    /// Госномера
    /// </summary>
    public string[] LicensePlate { get; set; }
}