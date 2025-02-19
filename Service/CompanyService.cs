using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        public CompanyService(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<IEnumerable<CompanyDto>> GetAllCompanies()
        {
            var companies = await _repository.Company.GetAllCompanies();
            return companies;
        }
        public async Task<CompanyDto> GetCompany(Guid id)
        {
            var company = await _repository.Company.GetCompany(id);
            if (company is null)
                throw new CompanyNotFoundException(id);
            return company;
        }
        public async Task<IEnumerable<CompanyWithEmployeesDto>> GetCompaniesWithEmployees()
        {
            var companies = await _repository.Company.GetCompaniesWithEmployees();
            return companies;
        }
        public async Task<CompanyDto> CreateCompany(CompanyForCreationDto company)
        {
            if (company.Employees is not null && company.Employees.Any())
                return await _repository.Company.CreateCompanyWithEmployees(company);
            else
                return await _repository.Company.CreateCompany(company);
        }
        public async Task<IEnumerable<CompanyDto>> GetByIds(IEnumerable<Guid> ids)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();
            var companies = await _repository.Company.GetByIds(ids);
            if (ids.Count() != companies.Count())
                throw new CollectionByIdsBadRequestException();
            return companies;
        }
        public async Task<(IEnumerable<CompanyDto> companies, string ids)>
        CreateCompanyCollection(IEnumerable<CompanyForCreationDto> companyCollection)
        {
            if (companyCollection is null)
                throw new CompanyCollectionBadRequest();
            var companies = await _repository.Company
            .CreateCompanyCollection(companyCollection);
            var ids = string.Join(",", companies.Select(c => c.CompanyId));
            return (companies, ids);
        }

    }
}
