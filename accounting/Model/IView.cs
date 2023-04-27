namespace accounting.Model;

public interface IView<T>
{
    void Show(List<T> entities);
    void ShowLastRows(List<T> entities, int n);
    void ShowById(List<T> entities, int id);

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void ShowError(string errorMessage)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(errorMessage);
        Console.ResetColor();
        Console.ReadKey();
    }
}