using AuthService.Domain.Entities;
using Dapper;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthService.Api.Bootstrap
{
    public static class DapperTypeMapping
    {
        public static void Map()
        {
            DapperExtensions.DapperAsyncExtensions.SqlDialect = new DapperExtensions.Sql.PostgreSqlDialect();
            DapperExtensions.DapperAsyncExtensions.SetMappingAssemblies(new[]
            {
               typeof (User).Assembly
            });
        }
    }
}
