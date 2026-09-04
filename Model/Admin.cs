using System;
using System.Collections.Generic;
using System.Text;

namespace SkyFlowManagement.Model;

public class Admin : User
{
    // Base construtor
    public Admin(int userId, string username, string passwordHash, string email, string firstName, string lastName, DateTime createdAt)
        : base(userId, username, passwordHash, "Admin", email, firstName, lastName, createdAt)
    { }


    // Method:

    // overriding the method template from the user class.
    public override void displayDashboard()
    {
        Console.WriteLine("Welcome " + FirstName + " !");
        Console.WriteLine(" Admin Dashboard");
        Console.WriteLine(" 1. Manage Flights");
        Console.WriteLine(" 2. View System Overview");
        Console.WriteLine(" 3. Manage Staff");
        Console.WriteLine(" 0. Logout");
    }
}
