using accounting;
using ConsoleTables;
using System.Collections.Generic;

Device device1 = new Device();
device1.InventoryNumber = "0001/23П";
device1.GetDeviceInformFromSystem();

// Создаем список устройств
List<Device> devices = new List<Device>();

// Заполняем список устройств данными
devices.Add(device1);
devices.Add(new Device(2, "Модель 2", "VID_2", "PID_2", "SN_2", "INV_2"));

// Выводим список устройств в виде таблицы
var table = new ConsoleTable("Инвентарный номер", "Модель", "Vendor ID\\Product ID", "Серийный номер");
foreach (Device device in devices)
{
    table.AddRow(device.InventoryNumber, device.Model, $"{device.Vid}\\{device.Pid}", device.SerialNumber);
}
table.Write();
