using accounting.Model;
using ConsoleTables;

namespace accounting.View;

public class DeviceView : IView<Device>
{
    public void Show(List<Device> devices)
    {
        var rows = ConsoleTable.From(devices);
        rows.Write();
    }

    public void ShowLastRows(List<Device> devices, int n)
    {
        var rows = ConsoleTable.From(devices.Skip(Math.Max(0, devices.Count - n)))
            .Configure(o => o.NumberAlignment = Alignment.Right);
        rows.Write();
    }

    public void ShowById(List<Device> devices, int id)
    {
        var _ = devices.FirstOrDefault(d => d.Id == id);
        if (_ != null)
        {
            var device = new List<Device>();
            device.Add(_);
            var table = ConsoleTable.From(device)
                .Configure(o => o.NumberAlignment = Alignment.Right);
            table.Write();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Device with ID {id} not found.");
            Console.ResetColor();
        }
    }
}