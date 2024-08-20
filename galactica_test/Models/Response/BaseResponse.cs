using galactica_test.Models.Enums;

namespace galactica_test.Models.Response;

/// <summary>
/// Типизированный ответ
/// </summary>
public class BaseResponse<T>
{
    /// <summary>
    /// Конструктор по умолчанию
    /// </summary>
    public BaseResponse()
    {
        Result = new BaseResult();
    }

    /// <summary>
    /// Результат выполнения операции
    /// </summary>
    public BaseResult Result { get; set; }
    
    /// <summary>
    /// Данные ответа
    /// </summary>
    public T? Data { get; set; }
}

/// <summary>
/// Типизированный ответ
/// </summary>
public class BaseResponse
{
    /// <summary>
    /// Конструктор по умолчанию
    /// </summary>
    public BaseResponse()
    {
        BaseResult = new BaseResult();
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="baseResult">Результат опеации</param>
    public BaseResponse(BaseResult baseResult)
    {
        BaseResult = baseResult;
    }

    /// <summary>
    /// Результат выполнения операции
    /// </summary>
    public BaseResult BaseResult { get; set; }
}

/// <summary>
/// Результат выполнения операции
/// </summary>
public class BaseResult
{
    /// <summary>
    /// Статус-код ответа
    /// </summary>
    public int Status { get; set; } = (int)ErrorCode.Success;

    /// <summary>
    /// Сообщение об операции
    /// </summary>
    public string? Message { get; set; } = "Success";
}