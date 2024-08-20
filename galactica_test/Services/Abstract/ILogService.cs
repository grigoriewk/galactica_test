namespace galactica_test.Services.Abstract;

/// <summary>
/// Сервис логирования
/// </summary>
public interface ILogService
{
    /// <summary>
    /// Залогировать данные
    /// </summary>
    /// <param name="exception">Ошибка</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task LogErrorAsync(Exception exception, CancellationToken cancellationToken);
}