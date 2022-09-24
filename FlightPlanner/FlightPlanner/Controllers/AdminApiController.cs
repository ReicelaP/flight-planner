using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [ApiController, Authorize]
    public class AdminApiController : ControllerBase
    {
        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            var flight = FlightStorage.GetFlight(id);
            if(flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }

        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlight(Flight flight)
        {
            if (!FlightStorage.IsValidValue(flight) || 
                !FlightStorage.IsValidDestinationAirport(flight) || 
                !FlightStorage.IsValidArrivalTime(flight))
            {
                return BadRequest();
            }

            if (FlightStorage.IsUniqueFlight(flight)) 
            {
                flight = FlightStorage.AddFlight(flight);
                return Created("", flight);
            }
            else
            {
                return Conflict();
            }
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlight(int id)
        {
            FlightStorage.DeleteFlight(id);
            return Ok();
        }
    }
}
