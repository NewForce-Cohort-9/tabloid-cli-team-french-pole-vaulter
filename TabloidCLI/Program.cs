using TabloidCLI.UserInterfaceManagers;

namespace TabloidCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            //change background color. uncomment desired color

            //Console.BackgroundColor = ConsoleColor.DarkGray;
            //Console.BackgroundColor = ConsoleColor.DarkGreen;
            //Console.BackgroundColor = ConsoleColor.DarkCyan;
            //Console.BackgroundColor = ConsoleColor.DarkBlue;
            //Console.BackgroundColor = ConsoleColor.DarkYellow;

            //Console.Clear();

            // MainMenuManager implements the IUserInterfaceManager interface
            IUserInterfaceManager ui = new MainMenuManager();
            while (ui != null)
            {
                // Each call to Execute will return the next IUserInterfaceManager we should execute
                // When it returns null, we should exit the program;
                ui = ui.Execute();
            }
        }
    }
}
