using System;

namespace FlightPlanner.Validations
{
    public static class FlightStorageValidators
    {
        public static bool IsValidValue(Flight flight)
        {
            return
            flight != null &&
            flight.From != null &&
            flight.To != null &&
            !string.IsNullOrEmpty(flight.From.Country) &&
            !string.IsNullOrEmpty(flight.From.City) &&
            !string.IsNullOrEmpty(flight.From.AirportCode) &&
            !string.IsNullOrEmpty(flight.To.Country) &&
            !string.IsNullOrEmpty(flight.To.City) &&
            !string.IsNullOrEmpty(flight.To.AirportCode) &&
            !string.IsNullOrEmpty(flight.Carrier) &&
            !string.IsNullOrEmpty(flight.DepartureTime) &&
            !string.IsNullOrEmpty(flight.ArrivalTime);
        }

        public static bool IsValidDestinationAirport(Flight flight)
        {
            if (flight.From.AirportCode.ToLower().Trim() == flight.To.AirportCode.ToLower().Trim())
            {
                return false;
            }

            return true;
        }

        public static bool IsValidArrivalTime(Flight flight)
        {
            DateTime departure = DateTime.Parse(flight.DepartureTime);
            DateTime arrival = DateTime.Parse(flight.ArrivalTime);
            int result = DateTime.Compare(arrival, departure);

            return result <= 0 ? false : true;
        }
    }
}
