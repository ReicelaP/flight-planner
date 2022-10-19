using FlightPlaner.Core.Models;
using FlightPlaner.Core.Services;
using FlightPlanner.Data;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace FlightPlanner.Services
{
    public class AirportService : EntityService<Airport>, IAirportService
    {
        public AirportService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public List<Airport> SearchAirport(string search)
        {
            var str = search.ToLower().Replace(" ", "");

            var airports = new List<Airport>();

            airports = _context.Airports.Where(a =>
                a.Country.ToLower().Contains(str) ||
                a.City.ToLower().Contains(str) ||
                a.AirportCode.ToLower().Contains(str))
                .DistinctBy(a => a.AirportCode)
                .ToList();

            return airports;
        }
    }
}
