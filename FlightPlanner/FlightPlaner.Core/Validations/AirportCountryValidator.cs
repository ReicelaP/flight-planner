using FlightPlaner.Core.Models;

namespace FlightPlaner.Core.Validations
{
    public class AirportCountryValidator : IAirportValidator
    {
        public bool IsValid(Airport airport)
        {
            return !string.IsNullOrEmpty(airport?.Country);
        }
    }
}
