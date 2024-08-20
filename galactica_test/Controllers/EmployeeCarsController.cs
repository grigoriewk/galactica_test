using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using galactica_test.Models.Enums;
using galactica_test.Models.Request;
using galactica_test.Models.Response;
using galactica_test.Services.Abstract;
using galactica_test.Utils;
using Microsoft.AspNetCore.Mvc;

namespace galactica_test.Controllers;

/// <summary>
/// Контроллер для работы с госномерами работников
/// </summary>
[ApiController]
[Route("[controller]")]
public class EmployeeCarsController : ControllerBase
{
    private readonly ISecurityService _securityService;
    private readonly ILogService _logService;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="securityService">Сервис охраны</param>
    /// <param name="logService">Сервис логирования</param>
    public EmployeeCarsController(ISecurityService securityService, ILogService logService)
    {
        _securityService = securityService;
        _logService = logService;
    }

    /// <summary>
    /// Привязать госномер к работнику
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<BaseResponse>> CreateEmployeeLicensePlate(
        [FromBody][Required] CreateEmployeeLicensePlateRequest request, 
        CancellationToken cancellationToken)
    {
        BaseResponse response;
        try
        {
            response = await _securityService.CreateEmployeeLicensePlateAsync(request, cancellationToken);
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
    
    /// <summary>
    /// Получить все Госномера с информацией о работниках
    /// </summary>
    /// <returns>Данные о всех госномерах с инфо о работниках</returns>
    [HttpGet]
    public async Task<ActionResult<IList<EmployeeCarResponse>>> GetAllLicensePlates(CancellationToken cancellationToken)
    {
        BaseResponse<IList<EmployeeCarResponse>> response;
        try
        {
            response = await _securityService.GetAllLicensePlatesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            response = new BaseResponse<IList<EmployeeCarResponse>>
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
    /// Проверить, привязан ли госномер к работнику
    /// </summary>
    [HttpGet("check-by-license")]
    public async Task<ActionResult<CheckLicensePlateResponse>> CheckByLicensePlate([FromQuery] string licensePlate, CancellationToken cancellationToken)
    {
        BaseResponse<CheckLicensePlateResponse> response;
        try
        {
            response = await _securityService.CheckByLicensePlateAsync(licensePlate, cancellationToken);
        }
        catch (Exception ex)
        {
            response = new BaseResponse<CheckLicensePlateResponse>
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
    /// Проверить, какие госномера принадлежат сотруднику
    /// </summary>
    [HttpGet("check-by-id")]
    public async Task<ActionResult<CheckEmployeeLicensePlatesResponse>> CheckByEmployeeId([FromQuery] long id, CancellationToken cancellationToken)
    {
        BaseResponse<CheckEmployeeLicensePlatesResponse> response;
        try
        {
            response = await _securityService.CheckByEmployeeIdAsync(id, cancellationToken);
        }
        catch (Exception ex)
        {
            response = new BaseResponse<CheckEmployeeLicensePlatesResponse>
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
    /// Изменить данные о госномере работника
    /// </summary>
    [HttpPut]
    public async Task<ActionResult<BaseResponse>> EditEmployeeLicensePlate(
        [FromBody][Required] EditEmployeeCarRequest request, 
        CancellationToken cancellationToken)
    {
        BaseResponse response;
        try
        {
            response = await _securityService.EditEmployeeCarAsync(request, cancellationToken);
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

    /// <summary>
    /// Отвязать госномер от работника
    /// </summary>
    [HttpDelete]
    public async Task<ActionResult<BaseResponse>> RemoveEmployeeLicensePlate(
        [FromBody][Required] RemoveEmployeeLicensePlateRequest request, 
        CancellationToken cancellationToken)
    {
        BaseResponse response;
        try
        {
            response = await _securityService.RemoveEmployeeLicensePlateAsync(request, cancellationToken);
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