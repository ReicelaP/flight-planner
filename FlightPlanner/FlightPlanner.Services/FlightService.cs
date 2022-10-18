using FlightPlaner.Core.Models;
using FlightPlaner.Core.Services;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FlightPlanner.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        public FlightService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public Flight GetCompleteFlightById(int id)
        {
            return _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .SingleOrDefault(f => f.Id == id);
        }

        public bool Exists(Flight flight)
        {
            return _context.Flights.Any(f => 
                f.ArrivalTime == flight.ArrivalTime &&
                f.DepartureTime == flight.DepartureTime &&
                f.Carrier == flight.Carrier &&
                f.From.AirportCode == flight.From.AirportCode &&
                f.To.AirportCode == flight.To.AirportCode);
        }

        public PageResult GetFlightsInfoFromSearch(SearchFlightsRequest request)
        {
            var pageResult = new PageResult();
            pageResult.Page = 0;
            pageResult.Items = _context.Flights
                .Include(flight => flight.From)
                .Include(flight => flight.To)
                .Where(flight => flight.From.AirportCode == request.From &&
                                 flight.To.AirportCode == request.To &&
                                 flight.DepartureTime.Substring(0, 10) == request.DepartureDate).ToArray();

            pageResult.TotalItems = pageResult.Items.Length;

            return pageResult;
        }
    }
}
