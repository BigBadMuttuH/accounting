using accounting.Model;

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
            var message = "A-Add, E-Edit, D-Delete, L-all records, I-show by ID. " +
                          "\tCtrl+C - exit.";
            _accountingView.ShowMessage(message);

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
        throw new NotImplementedException();
    }
    private bool GetId(out int id)
    {
        Console.Write("\nEnter Record ID: \t\t");
        int.TryParse(Console.ReadLine(), out id);
        if (id > 0) return false;
        _accountingView.ShowError("ID must be grate then 0 and not be null.");
        return true;
    }
}