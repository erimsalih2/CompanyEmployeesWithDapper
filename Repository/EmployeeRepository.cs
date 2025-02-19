using Contracts;
using Dapper;
using Repository.Queries;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DapperContext _context;
        public EmployeeRepository(DapperContext context) => _context = context;
        public async Task<IEnumerable<EmployeeDto>> GetEmployees(Guid companyId)
        {
            var query = EmployeeQuery.SelectEmployeesQuery;
            using (var connection = _context.CreateConnection())
            {
                var employees = await connection
                .QueryAsync<EmployeeDto>(query, new { companyId });
                return employees.ToList();
            }
        }
        public async Task<EmployeeDto> GetEmployee(Guid companyId, Guid id)
        {
            var query = EmployeeQuery.SelectEmployeeByIdAndCompanyIdQuery;
            using (var connection = _context.CreateConnection())
            {
                var param = new { companyId, id };
                var employee = await connection
                .QuerySingleOrDefaultAsync<EmployeeDto>(query, param);
                return employee;
            }
        }
        public async Task<EmployeeDto> CreateEmployeeForCompany(Guid companyId,
        EmployeeForCreationDto employeeDto)
        {
            var query = EmployeeQuery.InsertEmployeeWithOutputQuery;
            var param = new DynamicParameters(employeeDto);
            param.Add("id", companyId, DbType.Guid);
            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<Guid>(query, param);
                return new EmployeeDto(id, employeeDto.Name,
                employeeDto.Age, employeeDto.Position);
            }
        }
        public async Task<CompanyDto> CreateCompanyWithEmployees(CompanyForCreationDto
        company)
        {
            var query = CompanyQuery.InsertCompanyQuery;
            var param = new DynamicParameters(company);
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                using (var trans = connection.BeginTransaction())
                {
                    var id = await connection
                    .QuerySingleAsync<Guid>(query, param, transaction: trans);
                    var queryEmp = EmployeeQuery.InsertEmployeeNoOutputQuery;
                    var empList = company.Employees
                    .Select(e => new { e.Name, e.Age, e.Position, id });
                    await connection.ExecuteAsync(queryEmp, empList, transaction: trans);
                    trans.Commit();
                    return new CompanyDto(id, company.Name,
                    string.Join(", ", company.Address, company.Country));
                }
            }
        }


    }

}
