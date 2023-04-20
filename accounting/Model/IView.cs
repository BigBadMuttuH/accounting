namespace accounting.Model;

public interface IView<T>
{
    void Show(List<T> entities);
    void ShowLastRows(List<T> entities, int n);
    void ShowById(List<T> entities, int id);
    void ShowMessage(string message);
    void ShowError(string errorMessage);
}
