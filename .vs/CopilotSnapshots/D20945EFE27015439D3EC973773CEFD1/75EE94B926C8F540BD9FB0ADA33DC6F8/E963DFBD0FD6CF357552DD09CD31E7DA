using Microsoft.Data.SqlClient;
using SkyFlowManagement.Model;

namespace SkyFlowManagement.Database;

// SQLRepo commuicate with the database.
// It implements all the methods stored in the Datahadling class
// which is responsible for any database manipulation

public class SQLRepo : DataHandling
{
    // The connection string that tells the system where to find the SQL Server database
    private readonly string _connectionString;

    public SQLRepo(string connectionString)
    {
        _connectionString = connectionString;
    }
    //Creates a new database connection.
    private SqlConnection GetConnection() => new SqlConnection(_connectionString);


    // ************************************************************
    //                      --- User Methods ---
    // ************************************************************

    public User? GetUserByUsername(string username)
    {
        // Open a connection to the database ad closes automaticallly after implementation.
        using var conn = GetConnection();
        conn.Open();

        // Write the SQL query
        // @Username acts as a placeholder.
        using var cmd = new SqlCommand("SELECT * FROM Users WHERE Username = @Username", conn);
        cmd.Parameters.AddWithValue("@Username", username);

        using var reader = cmd.ExecuteReader();

        // If no user was found, return null.
        if (!reader.Read()) return null;

        // Gets role so we know which sub-class to implement.
        string role = reader["Role"].ToString() ?? "";

        // Gets informatio from the database and stores it in variables:
        int userId = (int)reader["UserId"];
        string uname = reader["Username"].ToString() ?? "";
        string passwordHash = reader["PasswordHash"].ToString() ?? "";
        string email = reader["Email"].ToString() ?? "";
        string firstName = reader["FirstName"].ToString() ?? "";
        string lastName = reader["LastName"].ToString() ?? "";
        DateTime createdAt = (DateTime)reader["CreatedAt"];

        // Calls sub-class depending on the role
        return role == "Admin"
            ? new Admin(userId, uname, passwordHash, email, firstName, lastName, createdAt)
            : new GateAgent(userId, uname, passwordHash, email, firstName, lastName, createdAt);
    }

    public void AddUser(string username, string passwordHash, string role, string email, string firstName, string lastName)
    {
        using var conn = GetConnection();
        conn.Open();

        // Added placeholers for cleaner use of data.
        using var cmd = new SqlCommand(@"
                INSERT INTO Users (Username, PasswordHash, Role, Email, FirstName, LastName, CreatedAt)
                VALUES (@Username, @PasswordHash, @Role, @Email, @FirstName, @LastName, @CreatedAt)", conn);

        cmd.Parameters.AddWithValue("@Username", username);
        cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
        cmd.Parameters.AddWithValue("@Role", role);
        cmd.Parameters.AddWithValue("@Email", email);
        cmd.Parameters.AddWithValue("@FirstName", firstName);
        cmd.Parameters.AddWithValue("@LastName", lastName);
        cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

        // Allows us to perform basic database manipulation without returning a row of data.
        // Just shows us which row was affected or add into the database as a integer.
        cmd.ExecuteNonQuery();
    }


    // ************************************************************
    //                     --- Flight Methods ---
    // ************************************************************

    public List<Flight> GetAllFlights()
    {
        // Start with an empty list and fill it as we read rows.
        var flights = new List<Flight>();

        using var conn = GetConnection();
        conn.Open();

        using var cmd = new SqlCommand("SELECT * FROM Flights", conn);
        using var reader = cmd.ExecuteReader();

        // Reads data from the database till the last row of the database.
        while (reader.Read())
        {
            flights.Add(MapFlight(reader));
        }

        return flights;
    }

    public Flight? GetFlightByNumber(string flightNumber)
    {
        using var conn = GetConnection();
        conn.Open();

        using var cmd = new SqlCommand("SELECT * FROM Flights WHERE FlightNumber = @FlightNumber", conn);
        cmd.Parameters.AddWithValue("@FlightNumber", flightNumber);

        using var reader = cmd.ExecuteReader();

        if (!reader.Read()) return null;
        return MapFlight(reader);
    }

    public void AddFlight(string flightNumber, string origin, string destination, DateTime departureTime, DateTime arrivalTime, int capacity, int gateAgentId)
    {
        using var conn = GetConnection();
        conn.Open();

        using var cmd = new SqlCommand(@"
                INSERT INTO Flights (FlightNumber, Origin, Destination, DepartureTime, ArrivalTime, Capacity, CurrentOccupancy, Status, GateAgentId)
                VALUES (@FlightNumber, @Origin, @Destination, @DepartureTime, @ArrivalTime, @Capacity, 0, 'Scheduled', @GateAgentId)", conn);

        cmd.Parameters.AddWithValue("@FlightNumber", flightNumber);
        cmd.Parameters.AddWithValue("@Origin", origin);
        cmd.Parameters.AddWithValue("@Destination", destination);
        cmd.Parameters.AddWithValue("@DepartureTime", departureTime);
        cmd.Parameters.AddWithValue("@ArrivalTime", arrivalTime);
        cmd.Parameters.AddWithValue("@Capacity", capacity);
        cmd.Parameters.AddWithValue("@GateAgentId", gateAgentId);

        cmd.ExecuteNonQuery();
    }

    public void UpdateFlightStatus(string flightNumber, string newStatus)
    {
        using var conn = GetConnection();
        conn.Open();

        using var cmd = new SqlCommand(@"
                UPDATE Flights 
                SET Status = @Status 
                WHERE FlightNumber = @FlightNumber", conn);

        cmd.Parameters.AddWithValue("@Status", newStatus);
        cmd.Parameters.AddWithValue("@FlightNumber", flightNumber);

        cmd.ExecuteNonQuery();
    }


    // ************************************************************
    //            --- Passenger & Booking Methods ---
    // ************************************************************

    public List<(Passenger Passenger, Booking Booking)> GetPassengersByFlightNumber(string flightNumber)
    {
        var results = new List<(Passenger, Booking)>();

        using var conn = GetConnection();
        conn.Open();

        // combines data from the tables Booking, Passanger and Flight.
        // The combination allows us to output data in one query.
        using var cmd = new SqlCommand(@"
                SELECT p.*, b.*
                FROM Bookings b
                JOIN Passengers p ON b.PassengerId = p.PassengerId
                JOIN Flights f ON b.FlightId = f.FlightId
                WHERE f.FlightNumber = @FlightNumber", conn);

        cmd.Parameters.AddWithValue("@FlightNumber", flightNumber);

        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            // Combines data from Passanger and Booking into one unit of data.
            results.Add((MapPassenger(reader), MapBooking(reader)));
        }

        return results;
    }

    public (Passenger Passenger, Booking Booking)? GetPassengerBySearch(string flightNumber, string search)
    {
        using var conn = GetConnection();
        conn.Open();

        // Search's either by PassengerId or PassportNumber.
        using var cmd = new SqlCommand(@"
                SELECT p.*, b.*
                FROM Bookings b
                JOIN Passengers p ON b.PassengerId = p.PassengerId
                JOIN Flights f ON b.FlightId = f.FlightId
                WHERE f.FlightNumber = @FlightNumber
                AND (CAST(p.PassengerId AS NVARCHAR) = @Search 
                OR p.PassportNumber = @Search)", conn);

        cmd.Parameters.AddWithValue("@FlightNumber", flightNumber);
        cmd.Parameters.AddWithValue("@Search", search);

        using var reader = cmd.ExecuteReader();

        if (!reader.Read()) return null;
        return (MapPassenger(reader), MapBooking(reader));
    }

    public void UpdateBookingStatus(int bookingId, string newStatus, DateTime? checkInTime, DateTime? boardingTime)
    {
        using var conn = GetConnection();
        conn.Open();

        using var cmd = new SqlCommand(@"
                UPDATE Bookings
                SET BookingStatus = @Status,
                    CheckInTime = @CheckInTime,
                    BoardingTime = @BoardingTime
                WHERE BookingId = @BookingId", conn);

        cmd.Parameters.AddWithValue("@Status", newStatus);

        // DBNull allows us to store null values within the database.
        // THis will be done till a action takes place between checkIn and boardingTime.
        cmd.Parameters.AddWithValue("@CheckInTime", (object?)checkInTime ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@BoardingTime", (object?)boardingTime ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@BookingId", bookingId);

        cmd.ExecuteNonQuery();
    }


    // ************************************************************
    //            --- Aircraft Methods ---
    // ************************************************************


    public List<Aircraft> GetAllAircraft()
    {
        var aircraft = new List<Aircraft>();

        using var conn = GetConnection();
        conn.Open();

        using var cmd = new SqlCommand("SELECT * FROM Aircraft", conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            aircraft.Add(new Aircraft(
                (int)reader["AircraftId"],
                reader["AircraftType"].ToString() ?? "",
                reader["RegistrationNumber"].ToString() ?? "",
                reader["Manufacturer"].ToString() ?? "",
                (int)reader["Capacity"],
                reader["Status"].ToString() ?? ""
            ));
        }

        return aircraft;
    }


    // ************************************************************
    //            --- Airport Methods ---
    // ************************************************************



    public List<Airport> GetAllAirports()
    {
        var airports = new List<Airport>();

        using var conn = GetConnection();
        conn.Open();

        using var cmd = new SqlCommand("SELECT * FROM Airports", conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            airports.Add(new Airport(
                (int)reader["AirportId"],
                reader["AirportCode"].ToString() ?? "",
                reader["AirportName"].ToString() ?? "",
                reader["City"].ToString() ?? "",
                reader["Country"].ToString() ?? ""
            ));
        }

        return airports;
    }


    // ************************************************************
    //                     --- Crew Methods ---
    // ************************************************************

    public List<Crew> GetAllCrew()
    {
        var crew = new List<Crew>();

        using var conn = GetConnection();
        conn.Open();

        using var cmd = new SqlCommand("SELECT * FROM Crew", conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            crew.Add(new Crew(
                (int)reader["CrewId"],
                (int)reader["UserId"],
                reader["CrewType"].ToString() ?? "",
                reader["LicenseNumber"].ToString() ?? "",
                (int)reader["YearsOfExperience"],
                reader["Status"].ToString() ?? ""
            ));
        }

        return crew;
    }

    public void AddCrew(int userId, string crewType, string licenseNumber, int yearsOfExperience)
    {
        using var conn = GetConnection();
        conn.Open();

        using var cmd = new SqlCommand(@"
                INSERT INTO Crew (UserId, CrewType, LicenseNumber, YearsOfExperience, Status)
                VALUES (@UserId, @CrewType, @LicenseNumber, @YearsOfExperience, 'Active')", conn);

        cmd.Parameters.AddWithValue("@UserId", userId);
        cmd.Parameters.AddWithValue("@CrewType", crewType);
        cmd.Parameters.AddWithValue("@LicenseNumber", licenseNumber);
        cmd.Parameters.AddWithValue("@YearsOfExperience", yearsOfExperience);

        cmd.ExecuteNonQuery();
    }


    // ************************************************************
    //                    --- Mapper Methods ---
    // ************************************************************

    // Converts a row within the database into a object.
    private Flight MapFlight(SqlDataReader reader) => new Flight(
        (int)reader["FlightId"],
        reader["FlightNumber"].ToString() ?? "",
        reader["Origin"].ToString() ?? "",
        reader["Destination"].ToString() ?? "",
        (DateTime)reader["DepartureTime"],
        (DateTime)reader["ArrivalTime"],
        (int)reader["Capacity"],
        (int)reader["CurrentOccupancy"],
        reader["Status"].ToString() ?? "",
        (int)reader["GateAgentId"]
    );

    private Passenger MapPassenger(SqlDataReader reader) => new Passenger(
        (int)reader["PassengerId"],
        (int)reader["UserId"],
        reader["PassportNumber"].ToString() ?? "",
        (DateTime)reader["DateOfBirth"],
        reader["Nationality"].ToString() ?? "",
        reader["ContactNumber"].ToString() ?? ""
    );

    private Booking MapBooking(SqlDataReader reader) => new Booking(
        (int)reader["BookingId"],
        (int)reader["FlightId"],
        (int)reader["PassengerId"],
        reader["SeatNumber"].ToString() ?? "",
        reader["BookingStatus"].ToString() ?? "",
        (DateTime)reader["BookingDate"],
        reader["CheckInTime"] == DBNull.Value ? null : (DateTime?)reader["CheckInTime"],
        reader["BoardingTime"] == DBNull.Value ? null : (DateTime?)reader["BoardingTime"]
    );
}