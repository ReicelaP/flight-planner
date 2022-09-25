using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        [Route("airports")]
        [HttpGet]
        public IActionResult GetAirport(string search)
        {
            var result = FlightStorage.SearchAirport(search);
            return Ok(result);
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult FindFlightById(int id)
        {
            if (FlightStorage.IsExistingFlight(id))
            {
                var result = FlightStorage.GetFlight(id);
                return Ok(result);
            }
            
            return NotFound();
            
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult FindFlights(SearchFlightsRequest request)              
        {
            if (SearchFlightsRequest.IsValidSearch(request) && 
                SearchFlightsRequest.IsValidDestinationAirport(request))
            {
                return Ok(FlightStorage.GetFlightsInfoFromSearch(request));
            }

            return BadRequest();         
        }
    }
}
