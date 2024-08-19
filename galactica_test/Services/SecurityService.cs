using galactica_test.Db;
using galactica_test.Db.Accessors.Abstract;
using galactica_test.Db.Entities;
using galactica_test.Models.Request;
using galactica_test.Models.Response;
using galactica_test.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace galactica_test.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly SecurityContext _context;
        public SecurityService(ISecurityContextAccessor context)
        {
            _context = context.CurrentContext;
        }

        /// <inheritdoc/>
        public async Task CreateEmployeeAsync(string name, string lastName)
        {
            var newEmployee = new EmployeeEntity
            {
                Name = name,
                LastName = lastName
            };

            _context.Employees.Add(newEmployee);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task EditEmployeeCarAsync(EditEmployeeCarRequest request)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task FireEmployeeAsync(long id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<IList<EmployeeModel>> GetAllEmployeesAsync()
        {
            var employees = await _context.Employees.ToListAsync();
            return employees
                .Select(x => new EmployeeModel(x.Id, x.Name, x.LastName))
                .ToList();
        }

        /// <inheritdoc/>
        public async Task<IList<EmployeeCarModel>> GetAllLicensePlatesAsync()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task LicensePlateToEmployeeAsync(LicensePlateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
