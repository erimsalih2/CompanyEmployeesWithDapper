using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDto>> GetAllCompanies();
        Task<CompanyDto> GetCompany(Guid id);
        Task<IEnumerable<CompanyWithEmployeesDto>> GetCompaniesWithEmployees();
        Task<CompanyDto> CreateCompany(CompanyForCreationDto company);
        Task<IEnumerable<CompanyDto>> GetByIds(IEnumerable<Guid> ids);

        Task<(IEnumerable<CompanyDto> companies, string ids)>
        CreateCompanyCollection(IEnumerable<CompanyForCreationDto> companyCollection);
    }
}
