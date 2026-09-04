using System;
using System.Collections.Generic;
using System.Text;

namespace SkyFlowManagement.Model;

public class Aircraft
{
    // Properties:

    public int AircraftId { get; private set; }
    public string AircraftType { get; private set; }
    public string RegistrationNumber { get; private set; }
    public string Manufacturer { get; private set; }
    public int Capacity { get; private set; }
    public string Status { get; private set; }

    // Constructor:

    public Aircraft(int aircraftId, string aircraftType, string registrationNumber, string manufacturer, int capacity, string status)
    {
        AircraftId = aircraftId;
        AircraftType = aircraftType;
        RegistrationNumber = registrationNumber;
        Manufacturer = manufacturer;
        Capacity = capacity;
        Status = status;
    }

    //Methods:

    public void updateStatus(string newStatus)
    {
        Status = newStatus;
    }
}