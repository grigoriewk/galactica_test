using galactica_test.Models.Request;
using galactica_test.Models.Response;

namespace galactica_test.Services.Abstract
{
    public interface ISecurityService
    {
        /// <summary>
        /// Зарегистрировать нового работника
        /// </summary>
        public Task CreateEmployeeAsync(string name, string lastName);

        /// <summary>
        /// Изменить данные о госномере работника
        /// </summary>
        public Task EditEmployeeCarAsync(EditEmployeeCarRequest request);

        /// <summary>
        /// Уволить работника
        /// </summary>
        public Task FireEmployeeAsync(long id);

        /// <summary>
        /// Получить всех работников
        /// </summary>
        /// <returns>Данные всех работников (без госномеров)</returns>
        public Task<IList<EmployeeModel>> GetAllEmployeesAsync();

        /// <summary>
        /// Получить все Госномера с инфо о работниках
        /// </summary>
        /// <returns>Данные о всех госномерах с инфо о работниках</returns>
        public Task<IList<EmployeeCarModel>> GetAllLicensePlatesAsync();
        
        /// <summary>
        /// Привязать госномер к работнику
        /// </summary>
        public Task LicensePlateToEmployeeAsync(LicensePlateRequest request);
    }
}
