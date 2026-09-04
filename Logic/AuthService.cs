using System;
using SkyFlowManagement.Database;
using SkyFlowManagement.Model;

namespace SkyFlowManagement.Logic
{
    // AuthService handles the validation
    // It sits between the UI and the database.

    public class AuthService
    {
        // Communicates to the database through the classes within the UI repository
        private readonly DataHandling _repository;

        public AuthService(DataHandling repository)
        {
            _repository = repository;
        }

        // Validates user credentials from the login screen.
        public User? Login(string username, string password)
        {
            // Step 1: Checks if the username exists in the database.
            User? user = _repository.GetUserByUsername(username);

            // Step 2: If the username isn't found then the program stops.
            if (user == null) return null;

            // Step 3: Hash the password the user types and compares it
            // to the hash stored in the database.
            string hashedInput = HashPassword(password);

            // Step 4: It references and uses validatePassword() method from the User class.
            // If they don't match, it return null, therefore the login failed
            if (!user.validatePassword(hashedInput)) return null;

            // Step 5: If all credentials are vaild
            // returns and call the user object.
            return user;
        }

        // Converts a plain-text password into a hash.
        // This must match exactly what LoginScreen.cs uses, and what's stored in the database.
        public static string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}