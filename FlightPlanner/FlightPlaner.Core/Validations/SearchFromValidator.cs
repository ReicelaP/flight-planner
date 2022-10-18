using FlightPlaner.Core.Models;

namespace FlightPlaner.Core.Validations
{
    public class SearchFromValidator : ISearchValidator
    {
        public bool IsValid(SearchFlightsRequest request)
        {
            return !string.IsNullOrEmpty(request.From);
        }
    }
}
