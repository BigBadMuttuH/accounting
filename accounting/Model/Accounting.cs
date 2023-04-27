namespace accounting.Model
{
    public class Accounting
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DeviceId { get; set; }
        public int ConnectionPermissionId { get; set; }

        public Accounting(int id, int userId, int deviceId, int connectionPermissionId)
        {
            Id = id;
            UserId = userId;
            DeviceId = deviceId;
            ConnectionPermissionId = connectionPermissionId;
        }
    }
}
