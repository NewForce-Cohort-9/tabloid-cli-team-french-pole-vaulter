using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class BackgroundColorManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;

        public BackgroundColorManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Background Color Selection");
            Console.WriteLine(" 1) Dark Cyan");
            Console.WriteLine(" 2) Dark Yellow");
            Console.WriteLine(" 3) Dark Blue");
            Console.WriteLine(" 4) Dark Gray");
            Console.WriteLine(" 5) Dark Green");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.Clear();
                    return this;
                case "2":
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.Clear();
                    return this;
                case "3":
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.Clear();
                    return this;
                case "4":
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Clear();
                    return this;
                case "5":
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.Clear();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;

            }
        }
    }
}
