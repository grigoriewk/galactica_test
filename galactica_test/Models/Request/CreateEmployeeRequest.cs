namespace galactica_test.Models.Request;

/// <summary>
/// Модель запроса создания нового работника
/// </summary>
public class CreateEmployeeRequest
{
    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; set; } 
    
    /// <summary>
    /// Фамилия
    /// </summary>
    public string LastName { get; set; }
}