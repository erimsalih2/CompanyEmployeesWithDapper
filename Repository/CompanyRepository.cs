using Contracts;
using Dapper;
using Entities.Models;
using Repository.Queries;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DapperContext _context;
        public CompanyRepository(DapperContext context) => _context = context;
        public async Task<IEnumerable<CompanyDto>> GetAllCompanies()
        {
            var query = CompanyQuery.SelectCompanyQuery;
            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<CompanyDto>(query);
                return companies.ToList();
            }
        }
        public async Task<CompanyDto> GetCompany(Guid id)
        {
            var query = CompanyQuery.SelectCompanyByIdQuery;
            using (var connection = _context.CreateConnection())
            {

                
            var company = await connection
            .QuerySingleOrDefaultAsync<CompanyDto>(query, new { companyId = id });
                return company;
            }
        }
        public async Task<IEnumerable<CompanyWithEmployeesDto>> GetCompaniesWithEmployees()
        {
            var query = CompanyQuery.SelectCompaniesWithEmployeesQuery;
            using (var connection = _context.CreateConnection())
            {
                var companyDict = new Dictionary<Guid, CompanyWithEmployeesDto>();
                var companies = await connection.QueryAsync<CompanyWithEmployeesDto,
               EmployeeDto, CompanyWithEmployeesDto>(
                query, (company, employee) =>
                {
                    if (!companyDict.TryGetValue(company.CompanyId, out var
            currentCompany))
                    {
                        currentCompany = company;
                        companyDict.Add(currentCompany.CompanyId, currentCompany);
                    }
                    currentCompany.Employees.Add(employee);
                    return currentCompany;
                }, splitOn: "CompanyId, EmployeeId"
                );
                return companies.Distinct().ToList();
            }
        }
        public async Task<CompanyDto> CreateCompany(CompanyForCreationDto company)
        {
            var query = CompanyQuery.InsertCompanyQuery;
            var param = new DynamicParameters(company);
            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<Guid>(query, param);
                return new CompanyDto(id, company.Name,
                string.Join(", ", company.Address, company.Country));
            }
        }
        public async Task<IEnumerable<CompanyDto>> GetByIds(IEnumerable<Guid> ids)
        {
            var query = CompanyQuery.SelectCompaniesForMultipleIdsQuery;
            using (var connection = _context.CreateConnection())
            {
                var companies = await connection
                .QueryAsync<CompanyDto>(query, new { ids });
                return companies.ToList();
            }
        }
        public async Task<IEnumerable<CompanyDto>> CreateCompanyCollection
         (IEnumerable<CompanyForCreationDto> companies)
        {
            var companyList = new List<CompanyDto>();
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                using (var trans = connection.BeginTransaction())
                {
                    foreach (var company in companies)
                    {
                        var query = CompanyQuery.InsertCompanyQuery;
                        var param = new DynamicParameters(company);
                        var id = await connection
                        .QuerySingleAsync<Guid>(query, param, transaction: trans);
                        companyList.Add(new CompanyDto(id, company.Name,
                        string.Join(", ", company.Address, company.Country)));
                    }

                    
                    trans.Commit();
                    return companyList;
                }
            }
        }

    }
}
