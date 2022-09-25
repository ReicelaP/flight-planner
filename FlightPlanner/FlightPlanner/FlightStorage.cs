using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlanner
{
    public static class FlightStorage
    {
        private static List<Flight> _flights = new List<Flight>();
        private static int _id = 0;

        public static Flight AddFlight(Flight flight)
        {
            flight.Id = ++_id;
            _flights.Add(flight);
            return flight;
        }

        public static Flight GetFlight(int id)
        {
            return _flights.FirstOrDefault(flight => flight.Id == id);
        }

        public static void Clear()
        {
            _flights.Clear();
            _id = 0;
        }

        public static bool IsUniqueFlight(Flight flight)
        {
            foreach (var f in _flights)
            {
                if(flight.From.Country == f.From.Country &&
                    flight.From.City == f.From.City &&
                    flight.From.AirportCode == f.From.AirportCode &&
                    flight.To.Country == f.To.Country &&
                    flight.To.City == f.To.City &&
                    flight.To.AirportCode == f.To.AirportCode &&
                    flight.Carrier == f.Carrier &&
                    flight.DepartureTime == f.DepartureTime &&
                    flight.ArrivalTime == f.ArrivalTime)
                {
                    return false;
                }
            }

            return true;
        }

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
            if(flight.From.Country.ToLower() == flight.To.Country.ToLower() ||
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

        public static void DeleteFlight(int id)
        {
            var flightDelete = _flights.FirstOrDefault(flight => flight.Id == id);
            _flights.Remove(flightDelete);
        }

        public static Airport[] SearchAirport(string search)
        {
            var str = search.ToLower().Replace(" ", "");

            var index1 = _flights.IndexOf(_flights.
                FirstOrDefault(f => f.From.Country.ToLower().Contains(str) || 
                                    f.From.City.ToLower().Contains(str) ||
                                    f.From.AirportCode.ToLower().Contains(str)));

            if (index1 > -1)
            {
                var result = _flights[index1].From;
                var arr = new Airport[] { result };
                return arr;
            }

            var index2 = _flights.IndexOf(_flights.
                FirstOrDefault(f => f.To.Country.ToLower().Contains(str) ||
                                    f.To.City.ToLower().Contains(str) ||
                                    f.To.AirportCode.ToLower().Contains(str)));

            if (index2 > -1)
            {
                var result = _flights[index2].To;
                var arr = new Airport[] { result };
                return arr;
            }

            return null;
        }

        public static bool IsExistingFlight(int id)
        {
            if (id <= _id && id >= 0)
            {
                return true;
            }

            return false;
        }

        public static PageResult GetFlightsInfoFromSearch(SearchFlightsRequest request)
        {
            PageResult pageResult = new PageResult();
            List<Flight> flightsList = new List<Flight>();

            foreach (var flight in _flights)
            {
                if (flight.From.AirportCode.ToString().ToLower() == request.From ||
                    flight.From.Country.ToString().ToLower() == request.From ||
                    flight.From.City.ToString().ToLower() == request.From)
                {
                    if (flight.To.AirportCode.ToString().ToLower() == request.To ||
                    flight.To.Country.ToString().ToLower() == request.To ||
                    flight.To.City.ToString().ToLower() == request.To)
                    {
                        if(flight.DepartureTime.Substring(0, 10) == request.DepartureDate)
                        {
                            pageResult.Page++;
                            pageResult.TotalItems++;
                            flightsList.Add(flight);
                        }
                    }
                }
            }

            pageResult.Items = flightsList.ToArray();

            if(pageResult.TotalItems > 0)
            {
                return pageResult;
            }

            else
            {
                pageResult.Page = 0;
                pageResult.TotalItems = 0;
                pageResult.Items = new Flight[0];
                return pageResult;
            }            
        }
    }
}
