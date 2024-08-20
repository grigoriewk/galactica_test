using galactica_test.Db;
using galactica_test.Db.Accessors.Abstract;
using galactica_test.Db.Entities;
using galactica_test.Services.Abstract;

namespace galactica_test.Services;

/// <inheritdoc />
public class LogService : ILogService
{
    private readonly SecurityContext _context;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="context">Контекст БД</param>
    public LogService(ISecurityContextAccessor context)
    {
        _context = context.CurrentContext;
    }

    /// <inheritdoc />
    public async Task LogErrorAsync(Exception exception, CancellationToken cancellationToken)
    {
        var logEntity = new LogEntity { ErrorMessage = exception.Message };
        await _context.Logs.AddAsync(logEntity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}