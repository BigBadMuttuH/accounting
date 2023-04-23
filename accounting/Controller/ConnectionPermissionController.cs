using accounting.Model;

namespace accounting.Controller;

public class ConnectionPermissionController : IController
{
    private readonly IDataAccess<ConnectionPermission> _connectionPermissionDataAccess;
    private readonly IView<ConnectionPermission> _connectionPermissionView;

    public ConnectionPermissionController(IDataAccess<ConnectionPermission> dataAccess,
        IView<ConnectionPermission> view) => (_connectionPermissionDataAccess, _connectionPermissionView) = (dataAccess, view);

    public void Start()
    {
        Console.Clear();
        try
        {
            var connectionPermissions = _connectionPermissionDataAccess.GetAll();
            var connectionPermissionList = new List<ConnectionPermission>();
            foreach (var _ in connectionPermissions) connectionPermissionList.Add(_);

            _connectionPermissionView.ShowLastRows(connectionPermissionList, 5);

            var message = "A-Add, E-Edit, D-Delete, L-all Permissions, I-show by ID. " +
                          "\tM-Main Menu.\tCtrl+C - exit.";
            _connectionPermissionView.ShowMessage(message);
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
                _connectionPermissionView.Show(connectionPermissionList);
                Console.ReadKey();
            }

            // Sub method for show device by id.
            void ShowById()
            {
                if (GetId(out var id)) return;
                _connectionPermissionView.ShowById(connectionPermissionList, id);
                Console.ReadKey();
            }
        }
        catch (Exception ex)
        {
            _connectionPermissionView.ShowError(ex.Message);
        }
    }

    public void Add()
    {
        if (GetId(out var id)) return;

        Console.Write("Enter Permission number: \t\t");
        var permissionNumber = Console.ReadLine();

        Console.Write("Enter permission date: \t\t\t");
        var permissionDate = DateTime.Parse(Console.ReadLine() ?? string.Empty);

        Console.Write("Enter permission registration number: \t");
        var registrationNumber = Console.ReadLine();

        Console.Write("Enter permission url: \t\t\t");
        var url = Console.ReadLine();

        var connectionPermission =
            new ConnectionPermission(id, permissionNumber, permissionDate, registrationNumber, url);

        IsNullOrWhiteSpaceEntity(connectionPermission);

        _connectionPermissionDataAccess.Add(connectionPermission);
        var message = $"Permission Added: {connectionPermission}";
        _connectionPermissionView.ShowMessage(message);
        Console.ReadKey();
    }


    public void Update()
    {
        if (GetId(out var id)) return;

        var connectionPermission = _connectionPermissionDataAccess.GetById(id);
        if (connectionPermission == null)
        {
            _connectionPermissionView.ShowError($"Permission with ID {id} not found.");
            return;
        }

        Console.Write($"Enter Permission number\t\t(Old value {connectionPermission.PermissionNumber})\t:");
        var permissionNumber = Console.ReadLine();

        Console.Write($"Enter Permission Date\t\t(Old value {connectionPermission.PermissionDate})\t:");
        var permissionDate = DateTime.Parse(Console.ReadLine() ?? string.Empty);

        Console.Write(
            $"Enter Permission registration number\t\t(Old value {connectionPermission.RegistrationNumber})\t:");
        var registrationNumber = Console.ReadLine();

        Console.Write($"Enter Permission URL\t (Old value {connectionPermission.Url})\t:");
        var url = Console.ReadLine();

        connectionPermission.PermissionNumber = permissionNumber;
        connectionPermission.PermissionDate = permissionDate;
        connectionPermission.RegistrationNumber = registrationNumber;
        connectionPermission.Url = url;

        var _ = new List<ConnectionPermission> { connectionPermission };
        _connectionPermissionDataAccess.Update(connectionPermission);
        _connectionPermissionView.ShowById(_, connectionPermission.Id);
        Console.WriteLine("Permission updated successfully.");
        Console.ReadKey();
    }

    public void Delete()
    {
        if (GetId(out var id)) return;

        try
        {
            _connectionPermissionDataAccess.Delete(id);
            Console.WriteLine("Permission deleted successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private bool GetId(out int id)
    {
        Console.Write("\nEnter Permission ID: \t\t\t");
        int.TryParse(Console.ReadLine(), out id);
        if (id > 0) return false;

        const string errorMessage = "ID must be grate then 0 and not be null.";
        _connectionPermissionView.ShowError(errorMessage);
        return true;
    }

    private void IsNullOrWhiteSpaceEntity(ConnectionPermission entity)
    {
        if (string.IsNullOrWhiteSpace(entity.PermissionNumber))
            throw new ArgumentException("Permission Number cannot be empty.");
    }
}