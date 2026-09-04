using System;
using System.Collections.Generic;
using System.Text;

namespace SkyFlowManagement.Model;

public class Crew
{
    public int CrewId { get; private set; }
    public int UserId { get; private set; }
    public string CrewType { get; private set; }
    public string LicenseNumber { get; private set; }
    public int YearsOfExperience { get; private set; }
    public string Status { get; private set; }

    public Crew(int crewId, int userId, string crewType, string licenseNumber, int yearsOfExperience, string status)
    {
        CrewId = crewId;
        UserId = userId;
        CrewType = crewType;
        LicenseNumber = licenseNumber;
        YearsOfExperience = yearsOfExperience;
        Status = status;
    }

    public void updateStatus(string newStatus)
    {
        Status = newStatus;
    }
}