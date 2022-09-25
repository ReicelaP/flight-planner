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
    }
}
