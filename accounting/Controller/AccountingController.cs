using accounting.DataBase;
using accounting.Model;
using accounting.View;

namespace accounting.Controller;

public class AccountingController : IController
{
    private readonly IDataAccess<Accounting> _accountingDataAccess;
    private readonly IView<Accounting> _accountingView;

    public AccountingController(IDataAccess<Accounting> accountingDataAccess, IView<Accounting> accountingView)
    {
        _accountingDataAccess = accountingDataAccess;
        _accountingView = accountingView;
    }

    public void Start()
    {
        Console.Clear();
        Console.WriteLine("Downloading data from Database. Wait please.");
        try
        {
            var accounting = _accountingDataAccess.GetAll();
            var accountingList = new List<Accounting>();
            foreach (var _ in accounting) accountingList.Add(_);

            Console.Clear();

            _accountingView.ShowLastRows(accountingList, 5);
            var message = @"
    A-Add, E-Edit, D-Delete, L-all records, I-show by ID.
    F1 - Devices; F2 - Users; F3 - Connection Permissions.
    Ctrl+C - exit.";
            _accountingView.ShowMessage(message);

            var key = Console.ReadKey().Key;

            switch (key)
            {
                case ConsoleKey.F1:
                    DeviceController();
                    break;
                case ConsoleKey.F2:
                    UserController();
                    break;
                case ConsoleKey.F3:
                    ConnectionPermissionController();
                    break;
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
                _accountingView.Show(accountingList);
                Console.ReadKey();
            }

            // Sub method for show device by id.
            void ShowById()
            {
                if (GetId(out var id)) return;
                _accountingView.ShowById(accountingList, id);
                Console.ReadKey();
            }
        }
        catch (Exception ex)
        {
            _accountingView.ShowError(ex.Message);
        }
    }


    public void Delete()
    {
        throw new NotImplementedException();
    }

    public void Update()
    {
        throw new NotImplementedException();
    }

    public void Add()
    {
        AddDevice();
        AddUser();
        AddConnectionPermission();

        void AddConnectionPermission()
        {
            throw new NotImplementedException();
        }

        void AddUser()
        {
            throw new NotImplementedException();
        }

        void AddDevice()
        {
            var deviceDataAccess = new DeviceDataAccess();
            var devicesList = new List<Device>(deviceDataAccess.GetAll());
            var deviceView = new DeviceView();
            var deviceController = new DeviceController(deviceDataAccess, deviceView);
            deviceView.ShowLastRows(devicesList, 5);
            deviceController.Add();
        }
    }

    private bool GetId(out int id)
    {
        Console.Write("\nEnter Record ID: \t\t");
        int.TryParse(Console.ReadLine(), out id);
        if (id > 0) return false;
        _accountingView.ShowError("ID must be grate then 0 and not be null.");
        return true;
    }

    public void DeviceController()
    {
        var deviceView = new DeviceView();
        var deviceDataAccess = new DeviceDataAccess();
        var deviceController = new DeviceController(deviceDataAccess, deviceView);
        deviceController.Start();
    }

    public void ConnectionPermissionController()
    {
        var connectionPermissionView = new ConnectionPermissionView();
        var connectionPermissionDataAccess = new ConnectionPermissionDataAccess();
        var connectionPermissionController =
            new ConnectionPermissionController(connectionPermissionDataAccess, connectionPermissionView);
        connectionPermissionController.Start();
    }

    public void UserController()
    {
        var userView = new UserView();
        var userDataAccess = new UserDataAccess();
        var userController = new UserController(userDataAccess, userView);
        userController.Start();
    }
}