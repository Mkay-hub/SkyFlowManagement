using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SkyFlowManagement.Model;

public class Booking
{
    // Properties:

    public int BookingId { get; private set; }
    public int FlightId { get; private set; }
    public int PassengerId { get; private set; }
    public string SeatNumber { get; private set; }
    public string BookingStatus { get; private set; }
    public DateTime BookingDate { get; private set; }
    public DateTime? CheckInTime { get; private set; }
    public DateTime? BoardingTime { get; private set; }

    // Constructor:

    public Booking(int bookingId, int flightId, int passengerId, string seatNumber, string bookingStatus, DateTime bookingDate, DateTime? checkInTime, DateTime? boardingTime)
    {
        BookingId = bookingId;
        FlightId = flightId;
        PassengerId = passengerId;
        SeatNumber = seatNumber;
        BookingStatus = bookingStatus;
        BookingDate = bookingDate;
        CheckInTime = checkInTime;
        BoardingTime = boardingTime;
    }

    //Method:

    public void updateStatus(string newStatus)
    {
        BookingStatus = newStatus;
    }

    public void checkIn()
    {
        BookingStatus = "CheckedIn";
        CheckInTime = DateTime.Now;
    }

    public void board()
    {
        BookingStatus = "Boarded";
        BoardingTime = DateTime.Now;
    }
}