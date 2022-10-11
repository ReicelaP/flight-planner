using FlightPlaner.Core.Models;

namespace FlightPlaner.Core.Validations
{
    public class AirportCityValidator : IAirportValidator
    {
        public bool IsValid(Airport airport)
        {
            return !string.IsNullOrWhiteSpace(airport?.City);
        }
    }
}
