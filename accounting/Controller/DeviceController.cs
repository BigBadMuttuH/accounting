using accounting.Model;

namespace accounting.Controller;

public class DeviceController : IController
{
    private readonly IDataAccess<Device> _deviceDataAccess;
    private readonly IView<Device> _deviceView;

    public DeviceController(IDataAccess<Device> dataAccess, IView<Device> view)
    {
        _deviceDataAccess = dataAccess;
        _deviceView = view;
    }

    public void Start()
    {
        Console.Clear();
        try
        {
            var _devices = _deviceDataAccess.GetAll();
            var devices = new List<Device>();
            foreach (var _device in _devices) devices.Add(_device);

            _deviceView.ShowLastRows(devices, 5);
            var message = "A-Add, E-Edit, D-Delete, L-all device, I-show by ID. " +
                          "\tM-Main Menu.\tCtrl+C - exit.";
            _deviceView.ShowMessage(message);

            var key = Console.ReadKey().Key;

            switch (key)
            {
                case ConsoleKey.L:
                    Show();
                    break;
                case ConsoleKey.I:
                    ShowById();
                    break;
                case ConsoleKey.M:
                    return;
                default:
                    HandleKeyPress(key);
                    break;
            }

            // Sub method for show all devices.
            void Show()
            {
                Console.Clear();
                _deviceView.Show(devices);
                Console.ReadKey();
            }

            // Sub method for show device by id.
            void ShowById()
            {
                Console.Write("\nEnter device ID: \t\t");
                var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                _deviceView.ShowById(devices, id);
                Console.ReadKey();
            }
        }
        catch (Exception ex)
        {
            _deviceView.ShowError(ex.Message);
        }
    }

    private void HandleKeyPress(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.A:
                AddDevice();
                break;
            case ConsoleKey.E:
                UpdateDevice();
                break;
            case ConsoleKey.D:
                DeleteDevice();
                break;
            default:
                Console.WriteLine($"Your choice: {key}.");
                break;
        }
    }

    private void AddDevice()
    {
        Console.Write("\nEnter device ID: \t\t");
        var id = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

        Console.Write("Enter device inventory number: \t");
        var inventoryNumber = Console.ReadLine();

        Console.Write("Add VID, PID, Model and Serial Number from connection USB-Flash.[Enter/Esc].\n");
        while (true)
        {
            var keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.Enter)
            {
                var device = new Device(id, inventoryNumber);
                _deviceDataAccess.Add(device);
                string message = $"Device Added: {device}";
                _deviceView.ShowMessage(message);
                Console.ReadKey();
                break;
            }

            if (keyInfo.Key == ConsoleKey.Escape)
            {
                Console.Write("Enter device model: \t\t");
                var model = Console.ReadLine();

                Console.Write("Enter device VID: \t\t");
                var vid = Console.ReadLine();

                Console.Write("Enter device PID: \t\t");
                var pid = Console.ReadLine();

                Console.Write("Enter device serial number: \t");
                var serialNumber = Console.ReadLine();

                var device = new Device(id, model, vid, pid, serialNumber, inventoryNumber);
                _deviceDataAccess.Add(device);
                string message = $"Device Added: {device}";
                _deviceView.ShowMessage(message);
                Console.ReadKey();
                break;
            }
        }
    }

    private void UpdateDevice()
    {
        Console.Write("\nEnter device ID to update\t\t\t\t:");
        var deviceId = int.Parse(Console.ReadLine());

        var device = _deviceDataAccess.GetById(deviceId);
        if (device == null)
        {
            Console.WriteLine($"Device with ID {deviceId} not found.");
            return;
        }

        Console.Write($"Enter device model\t\t(Old value {device.Model})\t:");
        var model = Console.ReadLine();

        Console.Write($"Enter device VID\t\t(Old value {device.Vid})\t:");
        var vid = Console.ReadLine();

        Console.Write($"Enter device PID\t\t(Old value {device.Pid})\t:");
        var pid = Console.ReadLine();

        Console.Write($"Enter device serial number\t(Old value {device.SerialNumber})\t:");
        var serialNumber = Console.ReadLine();

        Console.Write($"Enter device inventory number\t (Old value {device.InventoryNumber})\t:");
        var inventoryNumber = Console.ReadLine();

        device.Model = model;
        device.Vid = vid;
        device.Pid = pid;
        device.SerialNumber = serialNumber;
        device.InventoryNumber = inventoryNumber;

        var _ = new List<Device>();
        _.Add(device);
        _deviceDataAccess.Update(device);
        _deviceView.ShowById(_, device.Id);
        Console.WriteLine("Device updated successfully.");

        Console.ReadKey();
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