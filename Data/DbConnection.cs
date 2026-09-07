using Microsoft.Data.SqlClient;

namespace Productos.Api.Data;

public class DbConnection
{
    private readonly IConfiguration _configuration;

    public DbConnection(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public SqlConnection CreateConnection()
    {
        return new SqlConnection(
            _configuration.GetConnectionString("DefaultConnection")
        );
    }
}