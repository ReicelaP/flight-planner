using FlightPlaner.Core.Models;

namespace FlightPlaner.Core.Validations
{
    public class SearchToValidator : ISearchValidator
    {
        public bool IsValid(SearchFlightsRequest request)
        {
            return !string.IsNullOrEmpty(request.To);
        }
    }
}
