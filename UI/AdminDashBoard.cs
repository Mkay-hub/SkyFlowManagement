using SkyFlowManagement.Database;
using SkyFlowManagement.Logic;
using SkyFlowManagement.Model;

namespace SkyFlowManagement.UI
{
    public class AdminDashBoard
    {
        private readonly Admin _admin;
        private readonly FlightService _flightService;

        public AdminDashBoard(Admin admin, DataHandling repository)
        {
            _admin = admin;
            // AdminDashboard uses the FlightService class.
            // This allows this class to not have to communicate to the database directly.
            _flightService = new FlightService(repository);
        }

        public void Show()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();

                // Polymorphism: calls the method from the Admin sub-class
                // It therefore overrides the implementation.
                _admin.displayDashboard();

                Console.Write("\n Select option: ");
                string input = Console.ReadLine() ?? "";

                switch (input.Trim())
                {
                    case "1":
                        manageFlights();
                        break;
                    case "2":
                        viewSystemOverview();
                        break;
                    case "3":
                        manageStaff();
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("\n Invalid option. Press any key to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void manageFlights()
        {
            Console.Clear();
            Console.WriteLine(" Manage Flights");
            Console.WriteLine(" ---------------");
            Console.WriteLine(" 1. Add New Flight");
            Console.WriteLine(" 2. View All Flights");
            Console.WriteLine(" 3. Update Flight Status");
            Console.Write("\n Select option: ");
            string input = Console.ReadLine() ?? "";

            switch (input.Trim())
            {
                case "1":
                    _flightService.AddFlight();
                    break;
                case "2":
                    _flightService.ViewAllFlights();
                    break;
                case "3":
                    Console.Write(" Enter Flight Number: ");
                    string flightNumber = Console.ReadLine() ?? "";
                    _flightService.UpdateFlightStatus(flightNumber);
                    break;
                default:
                    Console.WriteLine(" Invalid option.");
                    break;
            }

            Console.WriteLine("\n Press any key to continue.");
            Console.ReadKey();
        }

        private void viewSystemOverview()
        {
            Console.Clear();
            Console.WriteLine(" System Overview — All Flights");
            Console.WriteLine(" ------------------------------");
            _flightService.ViewAllFlights();
            Console.WriteLine("\n Press any key to continue.");
            Console.ReadKey();
        }

        private void manageStaff()
        {
            Console.Clear();
            Console.WriteLine(" Manage Staff");
            Console.WriteLine(" -------------");
            Console.WriteLine(" 1. Add New Staff Member");
            Console.Write("\n Select option: ");
            string input = Console.ReadLine() ?? "";

            switch (input.Trim())
            {
                case "1":
                    _flightService.AddStaff();
                    break;
                default:
                    Console.WriteLine(" Invalid option.");
                    break;
            }

            Console.WriteLine("\n Press any key to continue.");
            Console.ReadKey();
        }
    }
}