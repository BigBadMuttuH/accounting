using System.Management;

namespace accounting.Model;

/// <summary>
///     Класс для USB устройства - flash-USB
///     GetDeviceInformFromSystem - получить информацию о подключенной флешки
/// </summary>
public class Device
{
    // Конструктор по умолчанию
    public Device()
    {
    }

    // Конструктор с параметрами
    public Device(int id, string model, string vid, string pid, string serialNumber, string inventoryNumber)
    {
        Id = id;
        Model = model;
        Vid = vid;
        Pid = pid;
        SerialNumber = serialNumber;
        InventoryNumber = inventoryNumber;
    }

    public int Id { get; set; } // Идентификатор в базе данных
    public string Model { get; set; } // Модель или производитель устройства
    public string Vid { get; set; } // Vendor ID (идентификатор производителя устройства)
    public string Pid { get; set; } // Product ID (идентификатор продукта устройства)
    public string SerialNumber { get; set; } // Серийный номер устройства
    public string InventoryNumber { get; set; } // Инвентарный номер устройства

    public override string ToString()
    {
        return $"InventoryNumber: {InventoryNumber}, Model: {Model}, {Vid}\\{Pid}, Serial Number: {SerialNumber}";
    }

    /// <summary>
    ///     Получить информацию о подключенной флешке в OS Windows
    /// </summary>
    public void GetDeviceInformFromSystem()
    {
        // Получаем информацию о подключенных USB-устройствах
        var usbDevices =
            new ManagementObjectSearcher(@"SELECT * FROM Win32_DiskDrive WHERE MediaType like ""Removable Media""")
                .Get();
        if (usbDevices.Count > 0)
        {
            foreach (var usbDevice in usbDevices)
            {
                Model = usbDevice["Model"].ToString();
                SerialNumber = usbDevice["PNPDeviceID"].ToString();
                SerialNumber = SerialNumber.Substring(SerialNumber.LastIndexOf("\\") + 1).Split("&")[0];
            }

            // Получаем VID и PID
            var str = $"SELECT * FROM Win32_USBHub WHERE DeviceID like '%{SerialNumber}%' AND Status='OK'";
            usbDevices = new ManagementObjectSearcher(str).Get();
            foreach (var usbDevice in usbDevices)
            {
                var deviceID = usbDevice["DeviceID"].ToString();
                if (deviceID.Contains("VID_")) Vid = deviceID.Substring(deviceID.IndexOf("VID_"), 8);
                if (deviceID.Contains("PID_")) Pid = deviceID.Substring(deviceID.IndexOf("PID_"), 8);
            }
        }
        else
        {
            Console.WriteLine("Не найдено подключенных флешэк.");
        }
    }
}