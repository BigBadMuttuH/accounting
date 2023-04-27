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
        throw new NotImplementedException();
    }

    public void Delete()
    {
        Console.WriteLine("Enter user Sam Account Name:\t");
        string? userName = Console.ReadLine();
        var user = new User(userName);
        _userDataAccess.Delete(user.Sid);
        _userView.ShowMessage($"User:{user} deleted.");
        Console.ReadKey();
    }

    public void Update()
    {
        Console.WriteLine("Enter user Sam Account Name:\t");
        string? userName = Console.ReadLine();
        var user = new User(userName);
        _userDataAccess.Update(user);
        _userView.ShowMessage($"User:{user} updated.");
        Console.ReadKey();
    }

    public void Add()
    {
        Console.WriteLine("Enter user Sam Account Name:\t");
        string? userName = Console.ReadLine();
        var user = new User(userName);
        _userDataAccess.Add(user);
        _userView.ShowMessage($"User:{user} added.");
        Console.ReadKey();
    }
}