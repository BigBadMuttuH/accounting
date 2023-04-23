namespace accounting.Model;

public static class ControllerExtensions
{
    public static void HandleKeyPress(this IController controller, ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.A:
                controller.Add();
                break;
            case ConsoleKey.E:
                controller.Update();
                break;
            case ConsoleKey.D:
                controller.Delete();
                break;
            default:
                Console.WriteLine($"Your choice: {key}.");
                break;
        }
    }
}