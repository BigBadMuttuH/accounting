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
            var devices = _deviceDataAccess.GetAll();
            var devicesList = new List<Device>();
            foreach (var _ in devices) devicesList.Add(_);

            _deviceView.ShowLastRows(devicesList, 5);
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
                    this.HandleKeyPress(key);
                    break;
            }

            // Sub method for show all devices.
            void Show()
            {
                Console.Clear();
                _deviceView.Show(devicesList);
                Console.ReadKey();
            }

            // Sub method for show device by id.
            void ShowById()
            {
                Console.Write("\nEnter device ID: \t\t");
                int.TryParse(Console.ReadLine(), out var id);
                id = id != 0 ? id : 0;
                _deviceView.ShowById(devicesList, id);
                Console.ReadKey();
            }
        }
        catch (Exception ex)
        {
            _deviceView.ShowError(ex.Message);
        }
    }

    public void Add()
    {
        Console.Write("\nEnter device ID: \t\t");
        int.TryParse(Console.ReadLine(), out var id);
        id = id != 0 ? id : 0;

        Console.Write("Enter device inventory number: \t");
        var inventoryNumber = Console.ReadLine();

        Console.Write("Add VID, PID, Model and Serial Number from connection USB-Flash.[Enter/Esc].\n");
        while (true)
        {
            var keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.Enter)
            {
                var device = new Device(id, inventoryNumber);
                IsNullOrWhiteSpaceEntity(device);
                _deviceDataAccess.Add(device);
                var message = $"Device Added: {device}";
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
                IsNullOrWhiteSpaceEntity(device);
                _deviceDataAccess.Add(device);
                var message = $"Device Added: {device}";
                _deviceView.ShowMessage(message);
                Console.ReadKey();
                break;
            }
        }
    }

    public void Update()
    {
        Console.Write("\nEnter device ID to update\t\t\t\t:");
        int.TryParse(Console.ReadLine(), out var id);
        id = id != 0 ? id : 0;
        if (id <=0 || string.IsNullOrWhiteSpace(id.ToString()))
            throw new ArithmeticException("ID не можежет быть 0, отрицательным или пустым.");

        var device = _deviceDataAccess.GetById(id);
        if (device == null)
        {
            Console.WriteLine($"Device with ID {id} not found.");
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

        var _ = new List<Device> { device };
        _deviceDataAccess.Update(device);
        _deviceView.ShowById(_, device.Id);
        Console.WriteLine("Device updated successfully.");

        Console.ReadKey();
    }

    public void Delete()
    {
        Console.Write("\nEnter device ID to delete: ");
        int.TryParse(Console.ReadLine(), out var id);
        id = id != 0 ? id : 0;
        if (id <=0 || string.IsNullOrWhiteSpace(id.ToString()))
            throw new ArithmeticException("ID must be grate then 0 and not be null.");

        try
        {
            _deviceDataAccess.Delete(id);
            Console.WriteLine("Device deleted successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    private void IsNullOrWhiteSpaceEntity(Device entity)
    {
        if (entity.Id <=0 || string.IsNullOrWhiteSpace(entity.Id.ToString()))
            throw new ArithmeticException("ID must be grate then 0 and not be null.");

        if (string.IsNullOrWhiteSpace(entity.SerialNumber))
            throw new ArgumentException("Serial Number cannot be empty.");
    }
}