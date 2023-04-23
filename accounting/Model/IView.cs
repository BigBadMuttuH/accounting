namespace accounting.Model;

public interface IView<T>
{
    void Show(List<T> entities);
    void ShowLastRows(List<T> connectionPermissions, int n);
    void ShowById(List<T> entities, int id);

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void ShowError(string errorMessage)
    {
        Console.WriteLine(errorMessage);
        Console.ReadKey();
    }
}