using accounting.Controller;
using accounting.DataBase;
using accounting.View;

Console.WriteLine("Initializing...");
while (true)
{
    var accountingView = new AccountingView();
    var accountingDataAccess = new AccountingDataAccess();
    var accountingController = new AccountingController(accountingDataAccess, accountingView);
    accountingController.Start();
}