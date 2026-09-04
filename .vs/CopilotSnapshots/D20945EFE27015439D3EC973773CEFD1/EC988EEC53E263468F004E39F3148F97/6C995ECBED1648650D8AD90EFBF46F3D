using SkyFlowManagement.Database;
using SkyFlowManagement.Logic;
using SkyFlowManagement.Model;

namespace SkyFlowManagement.UI
{
    public class LoginScreen
    {
        private readonly DataHandling _repository;

        public LoginScreen(DataHandling repository)
        {
            _repository = repository;
        }

        public User? Show()
        {
            Console.Clear();
            Console.WriteLine("+----------------------------------+");
            Console.WriteLine("|   SkyFlow Terminal Manager       |");
            Console.WriteLine("+----------------------------------+");

            Console.Write("\n Enter username: ");
            string username = Console.ReadLine() ?? "";

            Console.Write(" Enter password: ");
            string password = ReadHiddenInput();

            // AuthService handles the login logic.
            AuthService auth = new AuthService(_repository);
            User? user = auth.Login(username, password);

            if (user == null)
            {
                Console.WriteLine("\n Invalid username or password. Press any key to try again.");
                Console.ReadKey();
                return null;
            }

            Console.WriteLine($"\n Authentication successful. Role: {user.Role}");
            Thread.Sleep(1000);
            return user;
        }

        // Reads password input
        // And displays default symbols than showing the passwords as its typed.
        private string ReadHiddenInput()
        {
            string input = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    input += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    // Remove the last symbol from password input and erase the * on screen.
                    input = input[..^1];
                    Console.Write("\b \b");
                }
            }
            while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return input;
        }
    }
}