using System;
using System.Collections.Generic;
using System.Text;

namespace SkyFlowManagement.Model;

public class Passenger
{
    public int PassengerId { get; private set; }
    public int UserId { get; private set; }
    public string PassportNumber { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public string Nationality { get; private set; }
    public string ContactNumber { get; private set; }

    public Passenger(int passengerId, int userId, string passportNumber, DateTime dateOfBirth, string nationality, string contactNumber)
    {
        PassengerId = passengerId;
        UserId = userId;
        PassportNumber = passportNumber;
        DateOfBirth = dateOfBirth;
        Nationality = nationality;
        ContactNumber = contactNumber;
    }
}