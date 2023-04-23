﻿using accounting.Model;
using ConsoleTables;

namespace accounting.View;

public class DeviceView : IView<Device>
{
    public void Show(List<Device> devices)
    {
        var rows = ConsoleTable.From(devices);
        rows.Write();
    }

    public void ShowLastRows(List<Device> connectionPermissions, int n)
    {
        var rows = ConsoleTable.From(connectionPermissions.Skip(Math.Max(0, connectionPermissions.Count - n)))
            .Configure(o => o.NumberAlignment = Alignment.Right);
        rows.Write();
    }

    public void ShowById(List<Device> devices, int id)
    {
        var _ = devices.FirstOrDefault(d => d.Id == id);
        if (_ != null)
        {
            List<Device> device = new List<Device>();
            device.Add(_);
            var table = ConsoleTable.From<Device>(device)
                .Configure(o => o.NumberAlignment = Alignment.Right);
            table.Write();
        }
        else
        {
            Console.WriteLine($"Device with ID {id} not found.");
        }
    }
}