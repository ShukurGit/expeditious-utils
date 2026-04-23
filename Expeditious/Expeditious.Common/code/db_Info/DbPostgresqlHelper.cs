


namespace Expeditious.Common
{
    static public class DbPostgresqlHelper
    {
        static public String GetConnectionStringPosgresql(
            String? dbName = null,
            String host = "localhost",
            Int32 port = 5432,
            String username = "postgres",
            String password = "password",
            String schemaName = "public"
        )
        {
            dbName = dbName ?? "db_" + DateTime.Now.Ticks.ToString();

            // "Host=localhost;Port=5432;Database=your_database_name;Username=your_username;Password=your_password;Search Path=your_schema_name;";
            return $"Host={host};Port={port};Database={dbName};Username={username};Password={password};Search Path={schemaName};";

            // return $"Host={host};Port={port};Database={dbName};Username={username};Password={password};Current schema={schemaName};";
        }
    }
}
