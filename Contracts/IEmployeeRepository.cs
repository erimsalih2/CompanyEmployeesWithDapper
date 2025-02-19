using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeDto>> GetEmployees(Guid companyId);
        Task<EmployeeDto> GetEmployee(Guid companyId, Guid id);
        Task<EmployeeDto> CreateEmployeeForCompany(Guid companyId,
         EmployeeForCreationDto employeeDto);

    }

}
