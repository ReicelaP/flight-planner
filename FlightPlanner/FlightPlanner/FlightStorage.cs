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

        public static void DeleteFlight(int id)
        {
            var flightDelete = _flights.FirstOrDefault(flight => flight.Id == id);
            _flights.Remove(flightDelete);
        }

        public static bool IsUniqueFlight(Flight flight)
        {
            foreach (var f in _flights)
            {
                if (flight.From.Country == f.From.Country &&
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
            pageResult.Page = 0;
            pageResult.Items = _flights.
                Where(flight => flight.From.AirportCode == request.From &&
                                flight.To.AirportCode == request.To &&
                                flight.DepartureTime.Substring(0, 10) == request.DepartureDate).ToArray();
            
            pageResult.TotalItems = pageResult.Items.Length;
            return pageResult;         
        }
    }
}
