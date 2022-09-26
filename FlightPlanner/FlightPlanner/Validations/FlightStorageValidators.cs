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
            if (flight.From.Country.ToLower() == flight.To.Country.ToLower() ||
               flight.From.City.ToLower() == flight.To.City.ToLower() ||
               flight.From.AirportCode.ToLower() == flight.To.AirportCode.ToLower())
            {
                return false;
            }

            return true;
        }

        public static bool IsValidArrivalTime(Flight flight)
        {
            string[] start = flight.DepartureTime.Replace('-', ' ').Replace(':', ' ').Split(" ");
            DateTime departure = new DateTime(int.Parse(start[0]), int.Parse(start[1]), int.Parse(start[2]), int.Parse(start[3]), int.Parse(start[4]), 0);

            string[] end = flight.ArrivalTime.Replace('-', ' ').Replace(':', ' ').Split(" ");
            DateTime arrival = new DateTime(int.Parse(end[0]), int.Parse(end[1]), int.Parse(end[2]), int.Parse(end[3]), int.Parse(end[4]), 0);

            int result = DateTime.Compare(arrival, departure);

            return result <= 0 ? false : true;
        }
    }
}
