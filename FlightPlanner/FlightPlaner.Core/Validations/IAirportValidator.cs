using FlightPlaner.Core.Models;

namespace FlightPlaner.Core.Validations
{
    public interface IAirportValidator
    {
        bool IsValid(Airport airport);
    }
}
