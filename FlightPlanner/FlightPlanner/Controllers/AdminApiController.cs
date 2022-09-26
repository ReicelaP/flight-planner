using FlightPlanner.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [ApiController, Authorize]
    public class AdminApiController : ControllerBase
    {
        private static readonly object flightLock = new object();

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
            lock (flightLock)
            {
                if (!FlightStorageValidators.IsValidValue(flight) ||
                    !FlightStorageValidators.IsValidDestinationAirport(flight) ||
                    !FlightStorageValidators.IsValidArrivalTime(flight))
                {
                    return BadRequest();
                }

                if (!FlightStorage.IsUniqueFlight(flight))
                {
                    return Conflict();
                }

                flight = FlightStorage.AddFlight(flight);
            }
                     
            return Created("", flight);          
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlight(int id)
        {
            lock (flightLock)
            {
                FlightStorage.DeleteFlight(id);
            }
            
            return Ok();
        }
    }
}
