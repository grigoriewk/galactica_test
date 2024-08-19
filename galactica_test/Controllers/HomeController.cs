using galactica_test.Models.Request;
using galactica_test.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace galactica_test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ISecurityService _securityService;

        public HomeController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        /// <summary>
        /// Зарегистрировать нового работника
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> CreateEmployee([FromQuery] string name, [FromQuery] string lastName)
        {
            await _securityService.CreateEmployeeAsync(name, lastName);
            return Ok();
        }

        /// <summary>
        /// Получить всех работников
        /// </summary>
        /// <returns>Данные всех работников (без госномеров)</returns>
        [HttpGet("getEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var result = await _securityService.GetAllEmployeesAsync();
            return Ok(result);
        }

        /// <summary>
        /// Получить все Госномера с инфо о работниках
        /// </summary>
        /// <returns>Данные о всех госномерах с инфо о работниках</returns>
        [HttpGet("getLicences")]
        public async Task<IActionResult> GetAllLicensePlates()
        {
            var result = await _securityService.GetAllLicensePlatesAsync();
            return Ok(result);
        }

        /// <summary>
        /// Привязать госномер к работнику
        /// </summary>
        [HttpPost("licensePlateToEmploee")]
        public async Task<IActionResult> LicensePlateToEmployee(LicensePlateRequest request)
        {
            await _securityService.LicensePlateToEmployeeAsync(request);
            return Ok();
        }

        /// <summary>
        /// Уволить работника
        /// </summary>
        [HttpDelete("employee")]
        public async Task<IActionResult> FireEmployee([FromQuery] long id)
        {
            await _securityService.FireEmployeeAsync(id);
            return Ok();
        }

        /// <summary>
        /// Изменить данные о госномере работника
        /// </summary>
        [HttpPut("editEmployeeLicense")]
        public async Task<IActionResult> EditEmployeeCar(EditEmployeeCarRequest request)
        {
            await _securityService.EditEmployeeCarAsync(request);
            return Ok();
        }
    }
}
