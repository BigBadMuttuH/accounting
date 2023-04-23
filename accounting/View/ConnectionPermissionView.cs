using accounting.Model;
using ConsoleTables;

namespace accounting.View;

public class ConnectionPermissionView : IView<ConnectionPermission>
{
    public void Show(List<ConnectionPermission> connectionPermissions)
    {
        var rows = ConsoleTable.From(connectionPermissions);
        rows.Write();
    }

    public void ShowLastRows(List<ConnectionPermission> devices, int n)
    {
        var rows = ConsoleTable.From(devices.Skip(Math.Max(0, devices.Count - n)))
            .Configure(o => o.NumberAlignment = Alignment.Right);
        rows.Write();
    }

    public void ShowById(List<ConnectionPermission> connectionPermissions, int id)
    {
        var _ = connectionPermissions.FirstOrDefault(d => d.Id == id);
        if (_ != null)
        {
            connectionPermissions = new List<ConnectionPermission> { _ };
            var table = ConsoleTable.From<ConnectionPermission>(connectionPermissions)
                .Configure(o => o.NumberAlignment = Alignment.Right);
            table.Write();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Permission with ID {id} not found.");
            Console.ResetColor();
        }
    }
}