﻿namespace accounting.Model
{
    public class ConnectionPermission
    {
        public int Id { get; set; }         // ID в базе данных
        public string PermissionNumber { get; set; } // Номер предписания 
        public DateTime PermissionDate { get; set; } // Дата регистрации предписания
        public string RegistrationNumber { get; set; } // Регистрационный номер в системе ДО 
        public string Url { get; set; } // ссылка на http ресурс

        public ConnectionPermission() { }

        public ConnectionPermission(string permissionNumber, DateTime permissionDate, string registrationNumber, string url)
        {
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
