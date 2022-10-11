using FlightPlaner.Core.Models;

namespace FlightPlaner.Core.Validations
{
    public class CarrierValidator : IFlightValidator
    {
        public bool IsValid(Flight flight)
        {
            return !string.IsNullOrEmpty(flight?.Carrier);
        }
    }
}
