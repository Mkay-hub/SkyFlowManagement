using SkyFlowManagement.Model;

namespace SkyFlowManagement.Database
{
    // List of methods that will be used by other class specifically within the UI directory.
    // The are current empty till used.

    public interface DataHandling
    {
        // --- User ---

        // Finds a user in the database by their username else return a null value.
        User? GetUserByUsername(string username);

        // Adds a new staff member to the database.
        void AddUser(string username, string passwordHash, string role, string email, string firstName, string lastName);


        // --- Flights ---

        // Gets every flight in the system as a list.
        List<Flight> GetAllFlights();

        //Looks for specific flight depending on flight number, else return a null value
        Flight? GetFlightByNumber(string flightNumber);

        // Adds a brand new flight to the database.
        void AddFlight(string flightNumber, string origin, string destination, DateTime departureTime, DateTime arrivalTime, int capacity, int gateAgentId);

        // Changes a flight's status e.g. from "Scheduled" to "Boarding".
        void UpdateFlightStatus(string flightNumber, string newStatus);


        // --- Passengers & Bookings ---

        // Gets all passengers on a specific flight.
        // Added togther cause you need both when boarding.
        List<(Passenger Passenger, Booking Booking)> GetPassengersByFlightNumber(string flightNumber);

        // Search's for one specific passenger on a flight 
        (Passenger Passenger, Booking Booking)? GetPassengerBySearch(string flightNumber, string search);

        // Updates a passenger's booking status e.g. "CheckedIn" or "Boarded".
       // implemented without data till an action is taken.
        void UpdateBookingStatus(int bookingId, string newStatus, DateTime? checkInTime, DateTime? boardingTime);


        // --- Aircraft ---

        // Gets a list of all aircrafts in the database.
        List<Aircraft> GetAllAircraft();


        // --- Airport ---

        // Gets a list of all airports in the database.
        List<Airport> GetAllAirports();


        // --- Crew ---

        // Gets a list of all crew members.
        List<Crew> GetAllCrew();

        // Adds a new crew member to the database.
        void AddCrew(int userId, string crewType, string licenseNumber, int yearsOfExperience);
    }
}