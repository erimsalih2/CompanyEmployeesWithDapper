﻿using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IEmployeeService
    {
        Task<(IEnumerable<EmployeeDto> employees, MetaData metaData)>
        GetEmployees(Guid companyId, EmployeeParameters employeeParameters);

        Task<EmployeeDto> GetEmployee(Guid companyId, Guid id);
        Task<EmployeeDto> CreateEmployeeForCompany(Guid companyId,
        EmployeeForCreationDto employeeDto);
        Task DeleteEmployeeForCompany(Guid companyId, Guid employeeId);
        Task UpdateEmployeeForCompany(Guid companyId, Guid id,
        EmployeeForUpdateDto employee);
        Task<(IEnumerable<EmployeeDto> employees, MetaData metaData)>
        
    }
}
