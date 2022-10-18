using FlightPlaner.Core.Models;

namespace FlightPlaner.Core.Validations
{
    public class SearchDestinationValidator : ISearchValidator
    {
        public bool IsValid(SearchFlightsRequest request)
        {
            return request.From.ToLower() != request.To.ToLower();
        }
    }
}
