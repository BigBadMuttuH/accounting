using accounting.DataBase;
using accounting.Model;

namespace accounting.Controller;

public class DeviceController
{
    private readonly DeviceDataAccess _deviceDataAccess;

    public DeviceController()
    {
        _deviceDataAccess = new DeviceDataAccess();
    }

    public void HandleKeyPress(char key)
    {
        switch (key)
        {
            case 'a':
                AddDevice();
                break;
            case 'e':
                UpdateDevice();
                break;
            case 'r':
                DeleteDevice();
                break;
            default:
                Console.WriteLine($"Invalid key: {key}. Please try again.");
                break;
        }
    }

    private void AddDevice()
    {
        Console.Write("\nEnter device ID: \t\t");
        int id = Int32.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

        Console.Write("Enter device model: \t\t");
        var model = Console.ReadLine();

        Console.Write("Enter device VID: \t\t");
        var vid = Console.ReadLine();

        Console.Write("Enter device PID: \t\t");
        var pid = Console.ReadLine();

        Console.Write("Enter device serial number: \t");
        var serialNumber = Console.ReadLine();

        Console.Write("Enter device inventory number: \t");
        var inventoryNumber = Console.ReadLine();

        var device = new Device(id, model, vid, pid, serialNumber, inventoryNumber);
        _deviceDataAccess.Add(device);
        Console.WriteLine("Device added successfully.");
    }

    private void UpdateDevice()
    {
        Console.Write("\nEnter device ID to update: ");
        var deviceId = int.Parse(Console.ReadLine());

        var device = _deviceDataAccess.GetById(deviceId);
        if (device == null)
        {
            Console.WriteLine($"Device with ID {deviceId} not found.");
            return;
        }

        Console.Write("Enter device model: \t");
        var model = Console.ReadLine();

        Console.Write("Enter device VID: \t");
        var vid = Console.ReadLine();

        Console.Write("Enter device PID: \t");
        var pid = Console.ReadLine();

        Console.Write("Enter device serial number: \t");
        var serialNumber = Console.ReadLine();

        Console.Write("Enter device inventory number: \t");
        var inventoryNumber = Console.ReadLine();

        device.Model = model;
        device.Vid = vid;
        device.Pid = pid;
        device.SerialNumber = serialNumber;
        device.InventoryNumber = inventoryNumber;

        _deviceDataAccess.Update(device);
        Console.WriteLine("Device updated successfully.");
    }

    private void DeleteDevice()
    {
        Console.Write("\nEnter device ID to delete: ");
        var deviceId = int.Parse(Console.ReadLine());

        try
        {
            _deviceDataAccess.Delete(deviceId);
            Console.WriteLine("Device deleted successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}