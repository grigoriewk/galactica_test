namespace galactica_test.Models.Response;

/// <summary>
/// Информация о госномере
/// </summary>
public class CheckLicensePlateResponse
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
    /// Госномер
    /// </summary>
    public string LicensePlate { get; set; }
}