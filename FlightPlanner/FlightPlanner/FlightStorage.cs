using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FlightPlanner
{
    public static class FlightStorage
    {
        private static int _id = 0;

        public static void AddFlight(Flight flight, FlightPlannerDbContext _context)
        {
            flight.Id = ++_id;
            _context.Flights.Add(flight);
            _context.SaveChanges();
        }

        public static Flight GetFlight(int id, FlightPlannerDbContext _context)
        {
            return _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .FirstOrDefault(f => f.Id == id);
        }

        public static void DeleteFlight(int id, FlightPlannerDbContext _context)
        {
            var flight = _context.Flights.FirstOrDefault(f => f.Id == id);
            if (flight != null)
            {
                _context.Flights.Remove(flight);
                _context.SaveChanges();
            }
        }

        public static bool IsUniqueFlight(Flight flight, FlightPlannerDbContext _context)
        {
            if (_context.Flights.Where(f => f.From.Country == flight.From.Country && 
                    f.From.City == f.From.City &&
                    flight.From.AirportCode == f.From.AirportCode &&
                    flight.To.Country == f.To.Country &&
                    flight.To.City == f.To.City &&
                    flight.To.AirportCode == f.To.AirportCode &&
                    flight.Carrier == f.Carrier &&
                    flight.DepartureTime == f.DepartureTime &&
                    flight.ArrivalTime == f.ArrivalTime).Any())
            {
                return false;
            }

            return true;
        }

        public static Airport[] SearchAirport(string search, FlightPlannerDbContext _context)
        {
            var str = search.ToLower().Replace(" ", "");

            var flightFrom = _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .FirstOrDefault(f => 
                    f.From.Country.ToLower().Contains(str) ||
                    f.From.City.ToLower().Contains(str) ||
                    f.From.AirportCode.ToLower().Contains(str));

            if (flightFrom.From.Country != null)
            {
                var result = flightFrom.From;
                var arr = new Airport[] { result };
                return arr;
            }

            var flightTo = _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .FirstOrDefault(f => 
                    f.To.Country.ToLower().Contains(str) ||
                    f.To.City.ToLower().Contains(str) ||
                    f.To.AirportCode.ToLower().Contains(str));

            if (flightTo.To.Country != null)
            {
                var result = flightTo.To;
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

        public static PageResult GetFlightsInfoFromSearch(SearchFlightsRequest request, FlightPlannerDbContext _context)
        {
            PageResult pageResult = new PageResult();
            pageResult.Page = 0;
            pageResult.Items = _context.Flights.
                Where(flight => flight.From.AirportCode == request.From &&
                                flight.To.AirportCode == request.To &&
                                flight.DepartureTime.Substring(0, 10) == request.DepartureDate).ToArray();
            
            pageResult.TotalItems = pageResult.Items.Length;
            return pageResult;         
        }
    }
}
