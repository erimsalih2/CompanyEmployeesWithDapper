using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record CompanyWithEmployeesDto
    {
        public Guid CompanyId { get; init; }
        public string? Name { get; init; }
        public string? FullAddress { get; init; }
        public List<EmployeeDto> Employees { get; init; } = new List<EmployeeDto>();
    };
}
