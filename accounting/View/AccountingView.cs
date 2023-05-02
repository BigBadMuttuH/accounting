using accounting.Model;
using ConsoleTables;

namespace accounting.View;

public class AccountingView : IView<Accounting>
{
    public void Show(List<Accounting> entities)
    {
        var rows = ConsoleTable.From(entities);
        rows.Write();
    }

    public void ShowLastRows(List<Accounting> entities, int n)
    {
        var rows = ConsoleTable.From(entities.Skip(Math.Max(0, entities.Count - n)))
            .Configure(o => o.NumberAlignment = Alignment.Right);
        rows.Write();
    }

    public void ShowById(List<Accounting> entities, int id)
    {
        var _ = entities.FirstOrDefault(e => e.Id == id);
        if (_ != null)
        {
            var account = new List<Accounting>();
            account.Add(_);
            Console.WriteLine(_);
            var table = ConsoleTable.From(account)
                .Configure(a => a.NumberAlignment = Alignment.Right);
            table.Write();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Record {id} not found.");
            Console.ResetColor();
        }
    }
}