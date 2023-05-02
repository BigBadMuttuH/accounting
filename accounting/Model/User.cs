using System.DirectoryServices;
using System.Security.Cryptography;
using System.Security.Principal;
using Microsoft.VisualBasic.CompilerServices;

namespace accounting.Model
{
    public class User
    {
        public int Sid { get; set; }       // SID пользователя без домена
        public string DisplayName { get; set; } = null!;
        public string Department { get; set; } = null!;
        public string SamAccountName { get; set; } = null!;

        public User() { }

        public User(int sid, string displayName, string department, string samAccountName)
        {
            Sid = sid;
            DisplayName = displayName;
            Department = department;
            SamAccountName = samAccountName;
        }
        public User(string? samAccountName)
        {
            this.GetUserFromActiveDirectory(samAccountName);
        }

        public User? GetUserFromActiveDirectory(string? userName)
        {
            Config config = new Config();
            config.Domain = "regions.eais.customs.ru";

            // Выполняем поиск пользователя в Active Directory
            using (var root = new DirectoryEntry("LDAP://RootDSE"))
            {
                string domain = root.Properties["defaultNamingContext"].Value.ToString();
                using (var domainEntry = new DirectoryEntry($"LDAP://{config.Domain}"))
                {
                    using (var search = new DirectorySearcher(domainEntry))
                    {
                        search.Filter = $"(&(objectClass=user)(samaccountname={userName}))";
                        SearchResult result = search.FindOne();

                        if (result != null)
                        {
                            // Получаем свойства пользователя из Active Directory
                            string[] sidPars = new SecurityIdentifier((byte[])result.Properties["objectSid"][0], 0)
                                .ToString()
                                .Split("-");
                            string _ = sidPars[sidPars.Length - 1];
                            int sid = Int32.Parse(_);
                            //byte[] sidBytes = (byte[])result.Properties["objectSid"][0];
                            //int sid = BitConverter.ToInt32(sidBytes, 0);
                            string displayName = result.Properties["displayName"][0].ToString();
                            string department = result.Properties.Contains("department") ? result.Properties["department"][0].ToString() : "";
                            string samAccountName = result.Properties["samaccountname"][0].ToString();

                            // Возвращаем объект User с полученными свойствами
                            return new User(sid, displayName, department, samAccountName);
                        }
                    }
                }
            }

            // Если пользователь не найден в Active Directory, возвращаем null
            return null;
        }

        public override string ToString()
        {
            return $"{DisplayName}, Department: {Department}, SamAccountName: {SamAccountName}";
        }
    }
}

