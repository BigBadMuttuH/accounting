using accounting.Model;
using ConsoleTables;

namespace accounting.View;

public class UserView : IView<User>
{
    public void Show(List<User> users)
    {
        var rows = ConsoleTable.From(users);
        rows.Write();
    }

    public void ShowLastRows(List<User> users, int n)
    {
        var rows = ConsoleTable.From(users.Skip(Math.Max(0, users.Count - n)))
            .Configure(o => o.NumberAlignment = Alignment.Right);
        rows.Write();
    }

    public void ShowById(List<User> users, int sid)
    {
        var _ = users.FirstOrDefault(u => u.Sid == sid);
        if (_ == null)
        {
            var user = new List<User>();
            user.Add(_);
            var table = ConsoleTable.From(users)
                .Configure(o => o.NumberAlignment = Alignment.Right);
            table.Write();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"User with sid = {sid} not found.");
            Console.ResetColor();
        }
    }
}