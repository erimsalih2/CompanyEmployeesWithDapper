using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly DapperContext _dapperContext;
        private readonly Lazy<ICompanyRepository> _companyRepository;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;

        public RepositoryManager(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
            _companyRepository = new Lazy<ICompanyRepository>(() => new
           CompanyRepository(_dapperContext));
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new
           EmployeeRepository(_dapperContext));
        }

        public ICompanyRepository Company => _companyRepository.Value;
        public IEmployeeRepository Employee => _employeeRepository.Value;
    }
}
