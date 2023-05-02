using accounting.Model;

namespace accounting.Controller;

public class UserController : IController
{
    private readonly IDataAccess<User> _userDataAccess;
    private readonly IView<User> _userView;

    public UserController(IDataAccess<User> dataAccess, IView<User> view)
    {
        (_userDataAccess, _userView) = (dataAccess, view);
    }
    public void Start()
    {
        Console.Clear();
        try
        {
            var users = _userDataAccess.GetAll();
            var usersList = new List<User>();
            foreach (var _ in users) usersList.Add(_);

            _userView.ShowLastRows(usersList, 5);
            var message = "A-Add, E-Edit, D-Delete, L-all users, I-show by SID. " +
                          "\tM-Main Menu.\tCtrl+C - exit.";
            _userView.ShowMessage(message);

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
                _userView.Show(usersList);
                Console.ReadKey();
            }

            // Sub method for show device by id.
            void ShowById()
            {
                if (GetSid(out var id)) return;
                _userView.ShowById(usersList, id);
                Console.ReadKey();
            }
        }
        catch (Exception ex)
        {
            _userView.ShowError(ex.Message);
        }
    }

    public void Delete()
    {
        if (GetSid(out var sid)) return;
        _userDataAccess.Delete(sid);
        _userView.ShowMessage($"User with Sid:{sid} deleted.");
        Console.ReadKey();
    }

    public void Update()
    {
        Console.Write("\nEnter user Sam Account Name:\t");
        string? userName = Console.ReadLine();
        var user = new User(userName);
        _userDataAccess.Update(user);
        _userView.ShowMessage($"User:{user} updated.");
        Console.ReadKey();
    }

    public void Add()
    {
        Console.Write("\nEnter user Sam Account Name: \t");
        string? userName = Console.ReadLine();
        var user = new User().GetUserFromActiveDirectory(userName);
        _userDataAccess.Add(user);
        _userView.ShowMessage($"User:{user} added.");
        Console.ReadKey();
    }
    private bool GetSid(out int id)
    {
        Console.Write("\nEnter user SID:\t");
        int.TryParse(Console.ReadLine(), out id);
        if (id > 0) return false;
        _userView.ShowError("SID must be grate then 0 and not be null.");
        return true;
    }
}