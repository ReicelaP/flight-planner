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

            var airport = _context.Airports.ToList().FirstOrDefault(a =>
                a.Country.ToLower().Contains(str) ||
                a.City.ToLower().Contains(str) ||
                a.AirportCode.ToLower().Contains(str));

            return new Airport[] { airport };
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
