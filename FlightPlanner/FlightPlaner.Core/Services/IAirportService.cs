using FlightPlaner.Core.Models;
using System.Collections.Generic;

namespace FlightPlaner.Core.Services
{
    public interface IAirportService
    {
        List<Airport> SearchAirport(string search);
    }
}
