using galactica_test.Models.Request;
using galactica_test.Models.Response;

namespace galactica_test.Services.Abstract
{
    /// <summary>
    /// Сервис охраны
    /// </summary>
    public interface ISecurityService
    {
        /// <summary>
        /// Зарегистрировать нового работника
        /// </summary>
        public Task<BaseResponse> CreateEmployeeAsync(CreateEmployeeRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Изменить данные о госномере работника
        /// </summary>
        public Task<BaseResponse> EditEmployeeCarAsync(EditEmployeeCarRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Уволить работника
        /// </summary>
        public Task<BaseResponse> FireEmployeeAsync(long id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить всех работников
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Данные всех работников (без госномеров)</returns>
        public Task<BaseResponse<IList<EmployeeResponse>>> GetAllEmployeesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить все Госномера с инфо о работниках
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Данные о всех госномерах с инфо о работниках</returns>
        public Task<BaseResponse<IList<EmployeeCarResponse>>> GetAllLicensePlatesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Привязать госномер к работнике
        /// </summary>
        public Task<BaseResponse> CreateEmployeeLicensePlateAsync(CreateEmployeeLicensePlateRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Отвязать госномер от работника
        /// </summary>
        public Task<BaseResponse> RemoveEmployeeLicensePlateAsync(RemoveEmployeeLicensePlateRequest request, CancellationToken cancellationToken);
    }
}
