-- Inserts Test data that will be used to run the App.

USE dbSkyFlow;
GO

-- Clear existing data first to avoid duplicates.
DELETE FROM AuditLog;
DELETE FROM Notifications;
DELETE FROM FlightLog;
DELETE FROM Baggage;
DELETE FROM FlightAssignment;
DELETE FROM Bookings;
DELETE FROM Crew;
DELETE FROM Passengers;
DELETE FROM Flights;
DELETE FROM Aircraft;
DELETE FROM Airports;
DELETE FROM Users;
GO

-- Reset identity counters after delete.
DBCC CHECKIDENT ('Users', RESEED, 0);
DBCC CHECKIDENT ('Flights', RESEED, 0);
DBCC CHECKIDENT ('Passengers', RESEED, 0);
DBCC CHECKIDENT ('Bookings', RESEED, 0);
DBCC CHECKIDENT ('Aircraft', RESEED, 0);
DBCC CHECKIDENT ('Airports', RESEED, 0);
DBCC CHECKIDENT ('Crew', RESEED, 0);
GO

-- ************************************
--			- Users table -
-- ************************************
-- Passwords are SHA256 hashed then Base64 encoded — matching AuthService.HashPassword().
-- admin123  = jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=
-- agent123  = 2dulHGyZHtDjQBSMbNqGhFcFNMgAJMlH5OOXBT5OVHI=
-- Pass123   = iNQmb9tcolUrQuxFVHGBjX/346pc7bFTBY7T0NZCKZU=

INSERT INTO Users (Username, PasswordHash, Role, Email, FirstName, LastName)
VALUES
('admin',  'jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=', 'Admin',     'admin@skyflow.com',  'Nara',    'Smith'),
('agent1', '2dulHGyZHtDjQBSMbNqGhFcFNMgAJMlH5OOXBT5OVHI=', 'GateAgent', 'agent1@skyflow.com', 'Karabo',   'Dlamini'),
('agent2', '2dulHGyZHtDjQBSMbNqGhFcFNMgAJMlH5OOXBT5OVHI=', 'GateAgent', 'agent2@skyflow.com', 'Michael', 'Jordan'),
('pas1',   'iNQmb9tcolUrQuxFVHGBjX/346pc7bFTBY7T0NZCKZU=', 'GateAgent', 'pas1@email.com',     'Solomon','Smith'),
('pas2',   'iNQmb9tcolUrQuxFVHGBjX/346pc7bFTBY7T0NZCKZU=', 'GateAgent', 'pas2@email.com',     'John',   'Doe');
GO

-- ************************************
--			- Aircraft table -
-- ************************************
INSERT INTO Aircraft (AircraftType, RegistrationNumber, Manufacturer, Capacity, Status)
VALUES
('Boeing 737',   'ZS-ABC', 'Boeing', 150, 'Active'),
('Airbus A320',  'ZS-DEF', 'Airbus', 120, 'Active'),
('Boeing 787',   'ZS-GHI', 'Boeing', 200, 'Active');
GO

-- ************************************
--			- Airports table -
-- ************************************
INSERT INTO Airports (AirportCode, AirportName, City, Country)
VALUES
('CPT', 'Cape Town International',              'Cape Town',     'South Africa'),
('JHB', 'OR Tambo International',               'Johannesburg',  'South Africa'),
('DBN', 'King Shaka International',             'Durban',        'South Africa'),
('PLZ', 'Chief Dawid Stuurman International',   'Port Elizabeth','South Africa');
GO

-- ************************************
--			- Flights table -
-- ************************************

-- GateAgentId 2 = agent1, GateAgentId 3 = agent2

INSERT INTO Flights (FlightNumber, Origin, Destination, DepartureTime, ArrivalTime, Capacity, CurrentOccupancy, Status, GateAgentId)
VALUES
('SF102', 'CPT', 'JHB', DATEADD(hour, 2,  GETDATE()), DATEADD(hour, 4,  GETDATE()), 150, 2, 'Boarding',  2),
('SF221', 'DBN', 'CPT', DATEADD(hour, 4,  GETDATE()), DATEADD(hour, 6,  GETDATE()), 120, 1, 'Scheduled', 2),
('SF305', 'JHB', 'PLZ', DATEADD(hour, 6,  GETDATE()), DATEADD(hour, 8,  GETDATE()), 100, 0, 'Scheduled', 3);
GO

-- ************************************
--			- Passengers table -
-- ************************************

-- UserId 4 = pas1, UserId 5 = pas2
INSERT INTO Passengers (UserId, PassportNumber, DateOfBirth, Nationality, ContactNumber)
VALUES
(4, 'PASSPORT001', '1990-05-15', 'South African', '0821234567'),
(5, 'PASSPORT002', '1985-08-22', 'South African', '0837654321');
GO

-- ************************************
--			- Bookings table -
-- ************************************

-- FlightId 1 = SF102, PassengerId 1 = Solomon, PassengerId 2 = John

INSERT INTO Bookings (FlightId, PassengerId, SeatNumber, BookingStatus, BookingDate)
VALUES
(1, 1, '12A', 'Confirmed', DATEADD(day, -2, GETDATE())),
(1, 2, '14B', 'Confirmed', DATEADD(day, -1, GETDATE()));
GO

-- ************************************
--			- Crew table -
-- ************************************

-- UserId 2 = agent1 (acting as crew member.)

INSERT INTO Crew (UserId, CrewType, LicenseNumber, YearsOfExperience, Status)
VALUES
(2, 'Pilot',   'PIL001', 10, 'Active'),
(3, 'CoPilot', 'PIL002',  5, 'Active');
GO



-- ************************************************
-- NB: Run to confirm the data is loaded properly!
-- ************************************************

SELECT * FROM Users;
SELECT * FROM Flights;
SELECT * FROM Passengers;
SELECT * FROM Bookings;
SELECT * FROM Crew;
SELECT * FROM Bookings;