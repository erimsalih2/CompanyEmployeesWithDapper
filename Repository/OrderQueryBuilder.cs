using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public static class OrderQueryBuilder
    {
        public static string CreateOrderQuery<T>(string orderByQueryString, char alias)
        {
            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();
            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;
                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos
                .FirstOrDefault(pi => pi.Name
                 .Equals(propertyFromQueryName,
                StringComparison.InvariantCultureIgnoreCase));
                if (objectProperty == null)
                    continue;
                var direction = param.EndsWith(" desc") ? "desc" : "asc";
                orderQueryBuilder
                 .Append($"{alias}.{objectProperty.Name.ToString()} { direction}, ");
            }
            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            return string.IsNullOrEmpty(orderQuery) ? "name asc" : orderQuery;
        }
    }
}
