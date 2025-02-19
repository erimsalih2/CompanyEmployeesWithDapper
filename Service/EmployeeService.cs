using Contracts;
using Entities.Exceptions;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public EmployeeService(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<IEnumerable<EmployeeDto>> GetEmployees(Guid companyId)
        {
            var company = await _repository.Company.GetCompany(companyId);
            if (company is null)
                throw new CompanyNotFoundException(companyId);
            var employees = await _repository.Employee.GetEmployees(companyId);
            return employees;
        }
        public async Task<EmployeeDto> GetEmployee(Guid companyId, Guid id)
        {
            var company = await _repository.Company.GetCompany(companyId);
            if (company is null)
                throw new CompanyNotFoundException(companyId);
            var employee = await _repository.Employee.GetEmployee(companyId, id);
            if (employee is null)
                throw new EmployeeNotFoundException(id);
            return employee;
        }
        public async Task<EmployeeDto> CreateEmployeeForCompany(Guid companyId,
        EmployeeForCreationDto employeeDto)
        {
            var company = await _repository.Company.GetCompany(companyId);
            if (company is null)
                throw new CompanyNotFoundException(companyId);
            var employee = await _repository.Employee
            .CreateEmployeeForCompany(companyId, employeeDto);
            return employee;
        }

    }
}
