using galactica_test.Db;
using galactica_test.Db.Accessors.Abstract;
using galactica_test.Db.Entities;
using galactica_test.Models.Enums;
using galactica_test.Models.Request;
using galactica_test.Models.Response;
using galactica_test.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace galactica_test.Services
{
    /// <inheritdoc />
    public class SecurityService : ISecurityService
    {
        private readonly SecurityContext _context;
        
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст БД</param>
        public SecurityService(ISecurityContextAccessor context)
        {
            _context = context.CurrentContext;
        }

        /// <inheritdoc/>
        public async Task<BaseResponse> CreateEmployeeAsync(CreateEmployeeRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var newEmployee = new EmployeeEntity
            {
                Name = request.Name,
                LastName = request.LastName
            };

            _context.Employees.Add(newEmployee);
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                response.BaseResult = new BaseResult
                {
                    Status = (int)ErrorCode.InnerException,
                    Message = ex.Message
                };
            }


            return response;
        }

        /// <inheritdoc/>
        public async Task<BaseResponse> EditEmployeeCarAsync(EditEmployeeCarRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var isExist = await _context.Employees.AnyAsync(x => x.Id == request.EmployeeId, cancellationToken);
            if (!isExist)
            {
                response.BaseResult = new BaseResult
                {
                    Status = (int)ErrorCode.BadRequest,
                    Message = "Сотрудника с таким ID не существует"
                };
                return response;
            }
            
            var editLicensePlate = await _context.EmployeesCars
                .Where(x => x.EmployeeId == request.EmployeeId && x.LicensePlateNumber == request.OldLicensePlate)
                .FirstOrDefaultAsync(cancellationToken);
                
            if (editLicensePlate == null)
            {
                response.BaseResult = new BaseResult
                {
                    Status = (int)ErrorCode.BadRequest,
                    Message = $"Госномер {request.OldLicensePlate} не закреплён за сотрудником."
                };

                return response;
            }
                
            editLicensePlate.LicensePlateNumber = request.NewLicensePlate;
            await _context.SaveChangesAsync(cancellationToken);

            return response;
        }

        /// <inheritdoc/>
        public async Task<BaseResponse> FireEmployeeAsync(long id, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var employee = await _context.Employees.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (employee == null)
            {
                response.BaseResult = new BaseResult
                {
                    Status = (int)ErrorCode.BadRequest,
                    Message = "Сотрудника с таким ID не существует"
                };
                return response;
            }

            var employeeLicensePlates = await _context.EmployeesCars
                .Where(x => x.EmployeeId == id)
                .ToListAsync(cancellationToken);
            
            if (employeeLicensePlates.Count > 0)
            {
                _context.EmployeesCars.RemoveRange(employeeLicensePlates);
            } 
            
            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync(cancellationToken);
            
            return response;
        }

        /// <inheritdoc/>
        public async Task<BaseResponse<IList<EmployeeResponse>>> GetAllEmployeesAsync(CancellationToken cancellationToken)
        {
            var response = new BaseResponse<IList<EmployeeResponse>>();
            var employees = await _context.Employees.ToListAsync(cancellationToken);
            response.Data = employees
                .Select(x => new EmployeeResponse(x.Id, x.Name, x.LastName))
                .ToList();

            return response;
        }

        /// <inheritdoc/>
        public async Task<BaseResponse<IList<EmployeeCarResponse>>> GetAllLicensePlatesAsync(CancellationToken cancellationToken)
        {
            var response = new BaseResponse<IList<EmployeeCarResponse>>();

            var licenses = await _context.EmployeesCars
                .Include(x => x.Employee)
                .ToListAsync(cancellationToken);


            response.Data = licenses
                .GroupBy(x => x.Employee)
                .Select(x => new EmployeeCarResponse(
                    x.Select(s => s.LicensePlateNumber).ToArray(),
                    new EmployeeResponse(x.Key.Id, x.Key.Name, x.Key.LastName)))
                .ToList();

            return response;
        }

        /// <inheritdoc />
        public async Task<BaseResponse> CreateEmployeeLicensePlateAsync(CreateEmployeeLicensePlateRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            
            var existLicense = await _context.EmployeesCars
                .Include(x=>x.Employee)
                .SingleOrDefaultAsync(x => x.LicensePlateNumber == request.NewLicensePlate, cancellationToken);
            if (existLicense != null)
            {
                response = new BaseResponse(new BaseResult
                {
                    Status = (int)ErrorCode.BadRequest,
                    Message = $"Госномер уже зарегистрирован за работником с ID {existLicense.EmployeeId} " +
                              $"({existLicense.Employee.LastName} {existLicense.Employee.Name})"
                });
                return response;
            }
                
            var newLicensePlate = new EmployeesCarsEntity
            {
                EmployeeId = request.EmployeeId,
                LicensePlateNumber = request.NewLicensePlate
            };
                
            _context.EmployeesCars.Add(newLicensePlate);
            await _context.SaveChangesAsync(cancellationToken);

            return response;
        }

        /// <inheritdoc />
        public async Task<BaseResponse> RemoveEmployeeLicensePlateAsync(RemoveEmployeeLicensePlateRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var licensePlateToRemove = await _context.EmployeesCars
                .Where(x => x.EmployeeId == request.EmployeeId && x.LicensePlateNumber == request.LicensePlateToRemove)
                .FirstOrDefaultAsync(cancellationToken);
                
            if (licensePlateToRemove == null)
            {
                response.BaseResult = new BaseResult
                {
                    Status = (int)ErrorCode.BadRequest,
                    Message = $"Госномер {request.LicensePlateToRemove} не закреплён за сотрудником."
                };
                
                return response;
            }
            
            _context.EmployeesCars.Remove(licensePlateToRemove);
            await _context.SaveChangesAsync(cancellationToken);

            return response;
        }
    }
}
