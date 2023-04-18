using accounting.Model;
using accounting.DataBase;
using ConsoleTables;
using System.Collections.Generic;

namespace accounting.View
{



    public class DeviceView
    {
        public void PrintAllDevices()
        {
            IDataAccess<Device> deviceDataAccess = new DeviceDataAccess();
            var devices = deviceDataAccess.GetAll();
            var table1 = new ConsoleTable("Инвентарный номер", "Модель", "Vendor ID\\Product ID", "Серийный номер");
            foreach (Device device in devices)
            {
                table1.AddRow(device.InventoryNumber, device.Model, $"{device.Vid}\\{device.Pid}", device.SerialNumber);
            }
            table1.Write();
        }
    }
}
