using System;
using System.Collections.Generic;
using System.Text;

namespace SkyFlowManagement.Model;

public abstract class User
{
    // Properties:
    public int UserId { get; private set; }
    public string Username { get; private set; }
    public string Role { get; private set; }
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime CreatedAt { get; private set; }
    private string PasswordHash { get; set; }

    // Constructor:
    protected User(int userId, string username, string passwordHash, string role, string email, string firstName, string lastName, DateTime createdAt)
    {
        UserId = userId;
        Username = username;
        PasswordHash = passwordHash;
        Role = role;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        CreatedAt = createdAt;
    }
    // Mehtod: 

    public bool validatePassword(string Hash)
    {
        return PasswordHash == Hash;
    }
    // Template for displaying menu
    public abstract void displayDashboard();
}