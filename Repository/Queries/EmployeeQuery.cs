using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Queries
{
    public static class EmployeeQuery
    {
        public const string SelectEmployeesQuery =
        @"SELECT EmployeeId, [Name], Age, Position
        FROM Employees
        WHERE CompanyId = @companyId";
        public const string SelectEmployeeByIdAndCompanyIdQuery =
         @"SELECT EmployeeId, [Name], Age, Position
         FROM Employees
         WHERE EmployeeId = @id AND CompanyId = @companyId";
        public const string InsertEmployeeWithOutputQuery =
        @"INSERT INTO Employees (EmployeeId, Name, Age, Position, CompanyId)
        OUTPUT inserted.EmployeeId
        VALUES (default, @name, @age, @position, @id)";
        public const string InsertEmployeeNoOutputQuery =
        @"INSERT INTO Employees (EmployeeId, Name, Age, Position, CompanyId)
        VALUES (default, @name, @age, @position, @id)";

    }

}
