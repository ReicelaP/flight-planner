namespace FlightPlanner.Validations
{
    public class SearchRequestValidators
    {
        public static bool IsValidSearch(SearchFlightsRequest request)
        {
            return
                !string.IsNullOrEmpty(request.From) ||
                !string.IsNullOrEmpty(request.To) ||
                !string.IsNullOrEmpty(request.DepartureDate);
        }

        public static bool IsValidDestinationAirport(SearchFlightsRequest request)
        {
            if (request.From.ToLower() == request.To.ToLower())
            {
                return false;
            }

            return true;
        }
    }
}
