using accounting.Model;
using Npgsql;

namespace accounting.DataBase
{
    public class DeviceDataAccess : IDataAccess<Device>
    {
        public static PgSQLConnectionString connString = new PgSQLConnectionString
        {
            Host = "localhost",
            Port = "5432",
            Database = "acl",
            User = "postgres",
            Password = "postgres"
        };

        private readonly string connectionString = connString.ToString();

        public void Add(Device entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Device> GetAll()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM device", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new Device(
                                id: reader.GetInt32(0),
                                model: reader.GetString(1),
                                vid: reader.GetString(2),
                                pid: reader.GetString(3),
                                serialNumber: reader.GetString(4),
                                inventoryNumber: reader.GetString(5)
                            );
                        }
                    }
                }
            }
        }

        public Device GetById(int id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM device WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Device(
                                id: reader.GetInt32(0),
                                model: reader.GetString(1),
                                vid: reader.GetString(2),
                                pid: reader.GetString(3),
                                serialNumber: reader.GetString(4),
                                inventoryNumber: reader.GetString(5)
                            );
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public void Update(Device entity)
        {
            throw new NotImplementedException();
        }
    }
}
