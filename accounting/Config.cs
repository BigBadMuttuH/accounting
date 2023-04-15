using System;
using System.IO;
using Newtonsoft.Json;

namespace accounting
{
    public class Config
    {
        public string Domain { get; set; }
        public string DatabaseServer { get; set; }
        public string DatabaseUser { get; set; }
        public string DatabasePasswordHash { get; set; }

        public Config() { }

        public static Config Load(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("File not found.", fileName);
            }

            try
            {
                var json = File.ReadAllText(fileName);
                return JsonConvert.DeserializeObject<Config>(json);
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading configuration file.", ex);
            }
        }

        public void Save(string fileName)
        {
            try
            {
                var json = JsonConvert.SerializeObject(this);
                File.WriteAllText(fileName, json);
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving configuration file.", ex);
            }
        }
    }
}
