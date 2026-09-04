using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SkyFlowManagement.Model;

public class Flight
{
    // Property:
    public int FlightId { get; private set; }
    public string FlightNumber { get; private set; }
    public string Origin { get; private set; }
    public string Destination { get; private set; }
    public DateTime DepartureTime { get; private set; }
    public DateTime ArrivalTime { get; private set; }
    public int Capacity { get; private set; }
    public int CurrentOccupancy { get; private set; }
    public string Status { get; private set; }
    public int GateAgentId { get; private set; }

    // Constructor:
    public Flight(int flightId, string flightNumber, string origin, string destination, DateTime departureTime, DateTime arrivalTime, int capacity, int currentOccupancy, string status, int gateAgentId)
    {
        FlightId = flightId;
        FlightNumber = flightNumber;
        Origin = origin;
        Destination = destination;
        DepartureTime = departureTime;
        ArrivalTime = arrivalTime;
        Capacity = capacity;
        CurrentOccupancy = currentOccupancy;
        Status = status;
        GateAgentId = gateAgentId;
    }

    // Methods:

    public bool isFull()
    {
        if (CurrentOccupancy >= Capacity)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void updateOccupancy(int count)
    {
        CurrentOccupancy = count;
    }

    public void departFlight()
    {
        if (Status == "Departed")
        {
            Console.WriteLine(" Flight has already departed.");
            return;
        }
        Status = "Departed";
    }

    public void updateStatus(string newStatus)
    {
        Status = newStatus;
    }
}