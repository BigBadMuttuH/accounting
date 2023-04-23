using System.Diagnostics.CodeAnalysis;
using accounting.Controller;
using accounting.DataBase;
using accounting.View;

while (true)
{
    //var deviceView = new DeviceView();
    //var deviceDataAccess = new DeviceDataAccess();
    //var deviceController = new DeviceController(deviceDataAccess, deviceView);
    //deviceController.Start();


    var connectionPermissionView = new ConnectionPermissionView();
    var connectionPermissionDataAccess = new ConnectionPermissionDataAccess();
    var connectionPermissionController =
        new ConnectionPermissionController(connectionPermissionDataAccess, connectionPermissionView);
    connectionPermissionController.Start();
}