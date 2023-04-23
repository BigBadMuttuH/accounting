namespace accounting.DataBase;

public class PgSqlConnection
{
    public PgSqlConnection()
    {
        Host = "localhost";
        Port = "5432";
        Database = "acl";
        User = "postgres";
        Password = "postgres";
    }

    public string Host { get; set; }
    public string Port { get; set; }
    public string Database { get; set; }
    public string User { get; set; }
    public string Password { get; set; }

    public override string ToString()
    {
        return $"Server={Host};Port={Port};Database={Database};User Id={User};Password={Password};";
    }
}