namespace accounting.Model;

public interface IController
{
    void Start();
    /// <summary>
    /// Delete method
    /// </summary>
    void Delete();

    /// <summary>
    /// Update method
    /// </summary>
    void Update();
    /// <summary>
    /// Add entities method
    /// </summary>
    void Add();
}