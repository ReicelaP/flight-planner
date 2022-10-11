using FlightPlaner.Core.Models;

namespace FlightPlaner.Core.Validations
{
    public interface IFlightValidator
    {
        bool IsValid(Flight flight);
    }
}
