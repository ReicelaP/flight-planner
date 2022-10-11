using FlightPlaner.Core.Models;

namespace FlightPlaner.Core.Services
{
    public interface IFlightService : IEntityService<Flight>
    {
        Flight GetCompleteFlightById(int id);
        bool Exists(Flight flight);
    }
}
