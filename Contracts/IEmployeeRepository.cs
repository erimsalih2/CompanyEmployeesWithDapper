﻿using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        Task<PagedList<EmployeeDto>> GetEmployees(Guid companyId,
        EmployeeParameters employeeParameters);
        Task<EmployeeDto> GetEmployee(Guid companyId, Guid id);
        Task<EmployeeDto> CreateEmployeeForCompany(Guid companyId,
         EmployeeForCreationDto employeeDto);
        Task DeleteEmployee(Guid employeeId);
        Task UpdateEmployee(Guid employeeId, EmployeeForUpdateDto employee);
        
    }

}
