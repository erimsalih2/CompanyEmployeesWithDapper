using Entities.Models;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<CompanyDto>> GetAllCompanies();
        Task<CompanyDto> GetCompany(Guid id);
        Task<IEnumerable<CompanyWithEmployeesDto>> GetCompaniesWithEmployees();
        Task<CompanyDto> CreateCompany(CompanyForCreationDto company);
        Task<CompanyDto> CreateCompanyWithEmployees(CompanyForCreationDto company);
        Task<IEnumerable<CompanyDto>> GetByIds(IEnumerable<Guid> ids);
        Task<IEnumerable<CompanyDto>> CreateCompanyCollection
        (IEnumerable<CompanyForCreationDto> companies);
    }

}
