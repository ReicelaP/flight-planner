using FlightPlaner.Core.Models;

namespace FlightPlaner.Core.Validations
{
    public class SearchDeparturedateValidator : ISearchValidator
    {
        public bool IsValid(SearchFlightsRequest request)
        {
            return !string.IsNullOrEmpty(request.DepartureDate);
        }
    }
}
