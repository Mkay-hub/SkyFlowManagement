
-- It creates the SkyFlowDB database and all the tables.

-- Step 1: Create the database if it doesn't exist.
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'dbSkyFlow')
BEGIN
    CREATE DATABASE dbSkyFlow;
END
GO

USE dbSkyFlow;
GO


-- Users table: stores all staff (Admins and Gate Agents).
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
    CREATE TABLE Users (
        UserId          INT             PRIMARY KEY IDENTITY(1,1),
        Username        NVARCHAR(50)    NOT NULL UNIQUE,
        PasswordHash    NVARCHAR(255)   NOT NULL,
        Role            NVARCHAR(20)    NOT NULL,
        Email           NVARCHAR(100)   NOT NULL,
        FirstName       NVARCHAR(50)    NOT NULL,
        LastName        NVARCHAR(50)    NOT NULL,
        CreatedAt       DATETIME        NOT NULL DEFAULT GETDATE()
    );
END
GO

-- Aircraft table: stores aircraft details.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Aircraft')
BEGIN
    CREATE TABLE Aircraft (
        AircraftId          INT             PRIMARY KEY IDENTITY(1,1),
        AircraftType        NVARCHAR(50)    NOT NULL,
        RegistrationNumber  NVARCHAR(20)    NOT NULL UNIQUE,
        Manufacturer        NVARCHAR(50)    NOT NULL,
        Capacity            INT             NOT NULL,
        Status              NVARCHAR(20)    NOT NULL DEFAULT 'Active'
    );
END
GO

-- Airports table: stores airport details.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Airports')
BEGIN
    CREATE TABLE Airports (
        AirportId       INT             PRIMARY KEY IDENTITY(1,1),
        AirportCode     NVARCHAR(10)    NOT NULL UNIQUE,
        AirportName     NVARCHAR(100)   NOT NULL,
        City            NVARCHAR(50)    NOT NULL,
        Country         NVARCHAR(50)    NOT NULL
    );
END
GO


-- Flights table: depends on the Users
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Flights')
BEGIN
    CREATE TABLE Flights (
        FlightId            INT             PRIMARY KEY IDENTITY(1,1),
        FlightNumber        NVARCHAR(10)    NOT NULL UNIQUE,
        Origin              NVARCHAR(50)    NOT NULL,
        Destination         NVARCHAR(50)    NOT NULL,
        DepartureTime       DATETIME        NOT NULL,
        ArrivalTime         DATETIME        NOT NULL,
        Capacity            INT             NOT NULL,
        CurrentOccupancy    INT             NOT NULL DEFAULT 0,
        Status              NVARCHAR(20)    NOT NULL DEFAULT 'Scheduled',
        GateAgentId         INT             NOT NULL,
        FOREIGN KEY (GateAgentId) REFERENCES Users(UserId)
    );
END
GO

-- Passengers table: depends on Users
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Passengers')
BEGIN
    CREATE TABLE Passengers (
        PassengerId     INT             PRIMARY KEY IDENTITY(1,1),
        UserId          INT             NOT NULL,
        PassportNumber  NVARCHAR(20)    NOT NULL UNIQUE,
        DateOfBirth     DATE            NOT NULL,
        Nationality     NVARCHAR(50)    NOT NULL,
        ContactNumber   NVARCHAR(20)    NOT NULL,
        FOREIGN KEY (UserId) REFERENCES Users(UserId)
    );
END
GO

-- Crew table: depends on Users (UserId).
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Crew')
BEGIN
    CREATE TABLE Crew (
        CrewId              INT             PRIMARY KEY IDENTITY(1,1),
        UserId              INT             NOT NULL,
        CrewType            NVARCHAR(30)    NOT NULL,
        LicenseNumber       NVARCHAR(50)    NOT NULL UNIQUE,
        YearsOfExperience   INT             NOT NULL,
        Status              NVARCHAR(20)    NOT NULL DEFAULT 'Active',
        FOREIGN KEY (UserId) REFERENCES Users(UserId)
    );
END
GO

-- Bookings table: depends on Flights and Passengers.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Bookings')
BEGIN
    CREATE TABLE Bookings (
        BookingId       INT             PRIMARY KEY IDENTITY(1,1),
        FlightId        INT             NOT NULL,
        PassengerId     INT             NOT NULL,
        SeatNumber      NVARCHAR(10)    NOT NULL,
        BookingStatus   NVARCHAR(20)    NOT NULL DEFAULT 'Confirmed',
        BookingDate     DATETIME        NOT NULL DEFAULT GETDATE(),
        CheckInTime     DATETIME        NULL,
        BoardingTime    DATETIME        NULL,
        FOREIGN KEY (FlightId)      REFERENCES Flights(FlightId),
        FOREIGN KEY (PassengerId)   REFERENCES Passengers(PassengerId)
    );
END
GO

-- FlightAssignment table: depends on Flights, Aircraft, and Users.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'FlightAssignment')
BEGIN
    CREATE TABLE FlightAssignment (
        AssignmentId    INT         PRIMARY KEY IDENTITY(1,1),
        FlightId        INT         NOT NULL,
        AircraftId      INT         NOT NULL,
        PilotId         INT         NOT NULL,
        CoPilotId       INT         NOT NULL,
        AssignedDate    DATETIME    NOT NULL DEFAULT GETDATE(),
        FOREIGN KEY (FlightId)    REFERENCES Flights(FlightId),
        FOREIGN KEY (AircraftId)  REFERENCES Aircraft(AircraftId),
        FOREIGN KEY (PilotId)     REFERENCES Users(UserId),
        FOREIGN KEY (CoPilotId)   REFERENCES Users(UserId)
    );
END
GO

-- Baggage table: depends on Bookings.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Baggage')
BEGIN
    CREATE TABLE Baggage (
        BaggageId   INT             PRIMARY KEY IDENTITY(1,1),
        BookingId   INT             NOT NULL,
        Weight      DECIMAL(5,2)    NOT NULL,
        BaggageTag  NVARCHAR(20)    NOT NULL UNIQUE,
        Status      NVARCHAR(20)    NOT NULL DEFAULT 'Checked',
        FOREIGN KEY (BookingId) REFERENCES Bookings(BookingId)
    );
END
GO

-- FlightLog table: depends on Flights and Users.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'FlightLog')
BEGIN
    CREATE TABLE FlightLog (
        LogId       INT             PRIMARY KEY IDENTITY(1,1),
        FlightId    INT             NOT NULL,
        Action      NVARCHAR(50)    NOT NULL,
        PerformedBy INT             NOT NULL,
        PerformedAt DATETIME        NOT NULL DEFAULT GETDATE(),
        Details     NVARCHAR(500)   NULL,
        FOREIGN KEY (FlightId)      REFERENCES Flights(FlightId),
        FOREIGN KEY (PerformedBy)   REFERENCES Users(UserId)
    );
END
GO

-- Notification table: depends on Users.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Notifications')
BEGIN
    CREATE TABLE Notifications (
        NotificationId  INT             PRIMARY KEY IDENTITY(1,1),
        UserId          INT             NOT NULL,
        Message         NVARCHAR(500)   NOT NULL,
        Type            NVARCHAR(30)    NOT NULL,
        IsRead          BIT             NOT NULL DEFAULT 0,
        CreatedAt       DATETIME        NOT NULL DEFAULT GETDATE(),
        FOREIGN KEY (UserId) REFERENCES Users(UserId)
    );
END
GO

-- AuditLog table: depends on Users.
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AuditLog')
BEGIN
    CREATE TABLE AuditLog (
        AuditId     INT             PRIMARY KEY IDENTITY(1,1),
        UserId      INT             NOT NULL,
        Action      NVARCHAR(100)   NOT NULL,
        TableName   NVARCHAR(50)    NOT NULL,
        RecordId    INT             NOT NULL,
        OldValues   NVARCHAR(MAX)   NULL,
        NewValues   NVARCHAR(MAX)   NULL,
        Timestamp   DATETIME        NOT NULL DEFAULT GETDATE(),
        FOREIGN KEY (UserId) REFERENCES Users(UserId)
    );
END
GO