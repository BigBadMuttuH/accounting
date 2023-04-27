using accounting.Model;

namespace accounting.Controller;

public class DeviceController : IController
{
    private readonly IDataAccess<Device> _deviceDataAccess;
    private readonly IView<Device> _deviceView;

    public DeviceController(IDataAccess<Device> dataAccess, IView<Device> view)
    {
        (_deviceDataAccess, _deviceView) = (dataAccess, view);
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
                if (GetId(out var id)) return;
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
        if (GetId(out var id)) return;

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
                var message = $"Device added: {device}";
                _deviceView.ShowMessage(message);
                Console.ReadKey();
                break;
            }
        }
    }

    public void Update()
    {
        if (GetId(out var id)) return;

        var device = _deviceDataAccess.GetById(id);
        if (device == null)
        {
            _deviceView.ShowError($"Device with ID {id} not found.");
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
        _deviceView.ShowMessage("Device updated successfully.");
        Console.ReadKey();
    }

    public void Delete()
    {
        if (GetId(out var id)) return;

        try
        {
            _deviceDataAccess.Delete(id);
            _deviceView.ShowMessage("Device deleted successfully.");
        }
        catch (Exception ex)
        {
            _deviceView.ShowMessage(ex.Message);
        }
    }

    private bool GetId(out int id)
    {
        Console.Write("\nEnter Device ID: \t\t");
        int.TryParse(Console.ReadLine(), out id);
        if (id > 0) return false;
        _deviceView.ShowError("ID must be grate then 0 and not be null.");
        return true;
    }

    private void IsNullOrWhiteSpaceEntity(Device entity)
    {
        if (string.IsNullOrWhiteSpace(entity.SerialNumber))
            _deviceView.ShowError("Serial Number cannot be empty.");
    }
}