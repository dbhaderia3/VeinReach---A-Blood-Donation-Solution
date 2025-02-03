using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;



public class DatabaseService
{
    private readonly IConfiguration _configuration;

    public DatabaseService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public bool ConnectionMethod(IConfiguration configuration)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");

        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();  // Attempt to open the connection
                return connection.State == System.Data.ConnectionState.Open;
            }
        }
        catch (Exception)
        {
            // Log the exception if needed
            return false;
        }
       
    }
}
