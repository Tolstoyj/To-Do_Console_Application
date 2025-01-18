using HelloRider.Services;

namespace HelloRider
{
    class Program
    {
        static void Main(string[] args)
        {
            var taskManager = new TaskManager();
            var consoleUI = new ConsoleUI(taskManager);
            consoleUI.ShowMainMenu();
        }
    }
}

