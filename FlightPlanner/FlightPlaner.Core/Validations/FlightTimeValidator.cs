using FlightPlaner.Core.Models;
using System;

namespace FlightPlaner.Core.Validations
{
    public class FlightTimeValidator : IFlightValidator
    {
        public bool IsValid(Flight flight)
        {
            if(!string.IsNullOrEmpty(flight.ArrivalTime) &&
                !string.IsNullOrEmpty(flight.DepartureTime))
            {
                var arrival = DateTime.Parse(flight.ArrivalTime);
                var departure = DateTime.Parse(flight.DepartureTime);

                return arrival > departure;
            }

            return false;
        }
    }
}
