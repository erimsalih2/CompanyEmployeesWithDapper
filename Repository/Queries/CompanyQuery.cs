using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Queries
{
    public static class CompanyQuery
    {
        public const string SelectCompanyQuery =
        @"SELECT CompanyId, [Name],
         CONCAT([Address], ', ', Country) AS FullAddress
         FROM Companies
         ORDER BY [Name]";
    }

}
