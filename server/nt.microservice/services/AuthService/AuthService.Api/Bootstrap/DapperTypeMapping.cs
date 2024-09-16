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
