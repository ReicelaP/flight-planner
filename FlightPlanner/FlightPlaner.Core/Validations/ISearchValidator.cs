using FlightPlaner.Core.Models;

namespace FlightPlaner.Core.Validations
{
    public interface ISearchValidator
    {
        bool IsValid(SearchFlightsRequest request);
    }
}
