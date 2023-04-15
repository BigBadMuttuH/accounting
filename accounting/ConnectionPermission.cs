namespace accounting
{
    public class ConnectionPermission
    {
        public int Id { get; set; }         // ID в базе данных
        public int DeviceId { get; set; }   // ID устройства в базе данных
        public int UserId { get; set; }     // ID Пользователя в базе дынных
        public string PermissionNumber { get; set; } // Номер предписания 
        public DateTime PermissionDate { get; set; } // Дата регистрации предписания
        public string RegistrationNumber { get; set; } // Регистрационный номер в системе ДО 
        public string Url { get; set; } // ссылка на http ресурс

        public ConnectionPermission() { }

        public ConnectionPermission(int deviceId, int userId, string permissionNumber, DateTime permissionDate, string registrationNumber, string url)
        {
            DeviceId = deviceId;
            UserId = userId;
            PermissionNumber = permissionNumber;
            PermissionDate = permissionDate;
            RegistrationNumber = registrationNumber;
            Url = url;
        }
        
        public override string ToString()
        {
            return $"Permission Number: {PermissionNumber}, Permission Date: {PermissionDate}, URL: {Url}";
        }
    }
}

