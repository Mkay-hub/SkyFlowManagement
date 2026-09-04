using System;
using System.Collections.Generic;
using System.Text;

namespace SkyFlowManagement.Model
{
    public class GateAgent : User
    {
        // Base constructor:
        public GateAgent(int userId, string username, string passwordHash, string email, string firstName, string lastName, DateTime createdAt)
            : base(userId, username, passwordHash, "GateAgent", email, firstName, lastName, createdAt)
        { }

        //Method:

        // overriding the template for this method from the users class.
        public override void displayDashboard()
        {
            Console.WriteLine("Welcome " + FirstName + " !");
            Console.WriteLine(" Gate Agent Dashboard");
            Console.WriteLine(" 1. View Flight Manifest");
            Console.WriteLine(" 2. Passenger Check-In");
            Console.WriteLine(" 3. Boarding Gate");
            Console.WriteLine(" 0. Logout");
        }
    }
}
