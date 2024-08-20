using System.ComponentModel.DataAnnotations;
using galactica_test.Models.Enums;
using galactica_test.Models.Request;
using galactica_test.Models.Response;
using galactica_test.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace galactica_test.Controllers;

/// <summary>
/// Контроллер для работы с работниками
/// </summary>
[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly ISecurityService _securityService;
    private readonly ILogService _logService;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="securityService">Сервис охраны</param>
    /// <param name="logService">Сервис логирования</param>
    public EmployeeController(ISecurityService securityService, ILogService logService)
    {
        _securityService = securityService;
        _logService = logService;
    }

    /// <summary>
    /// Зарегистрировать нового работника
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<BaseResponse>> CreateEmployee([FromBody] [Required] CreateEmployeeRequest request, CancellationToken cancellationToken)
    {
        BaseResponse response;
        try
        {
            response = await _securityService.CreateEmployeeAsync(request, cancellationToken);
        }
        catch (Exception ex)
        {
            response = new BaseResponse(new BaseResult
            {
                Status = (int)ErrorCode.InnerException,
                Message = "Внутренняя ошибка сервиса"
            });

            await _logService.LogErrorAsync(ex, cancellationToken);
        }

        return StatusCode(response.BaseResult.Status, response);
    }

    /// <summary>
    /// Получить всех работников 
    /// </summary>
    /// <returns>Данные всех работников (без госномеров)</returns>
    [HttpGet]
    public async Task<ActionResult<IList<EmployeeResponse>>> GetAllEmployees(CancellationToken cancellationToken)
    {
        BaseResponse<IList<EmployeeResponse>> response;
        try
        {
            response = await _securityService.GetAllEmployeesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            response = new BaseResponse<IList<EmployeeResponse>>
            {
                Result = new BaseResult
                {
                    Status = (int)ErrorCode.InnerException,
                    Message = "Внутренняя ошибка сервиса"
                }
            };

            await _logService.LogErrorAsync(ex, cancellationToken);
        }

        return StatusCode(response.Result.Status, response);
    }
    
    /// <summary>
    /// Уволить работника (каскадное удаление госномеров)
    /// </summary>
    [HttpDelete]
    public async Task<ActionResult<BaseResponse>> FireEmployee([FromQuery] [Required] long id, CancellationToken cancellationToken)
    {
        BaseResponse response;
        try
        {
            response = await _securityService.FireEmployeeAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            response = new BaseResponse
            {
                BaseResult = new BaseResult 
                { 
                    Status = (int)ErrorCode.InnerException, 
                    Message = "Внутренняя ошибка сервиса" 
                }
            };
                
            await _logService.LogErrorAsync(ex, cancellationToken);
        }
            
        return StatusCode(response.BaseResult.Status, response);
    }
}