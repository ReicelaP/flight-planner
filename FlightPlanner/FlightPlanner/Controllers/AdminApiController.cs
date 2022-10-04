using FlightPlanner.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [ApiController, Authorize]
    public class AdminApiController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;

        public AdminApiController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        private static readonly object flightLock = new object();

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            var flight = FlightStorage.GetFlight(id, _context);

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

                if (!FlightStorage.IsUniqueFlight(flight, _context))
                {
                    return Conflict();
                }

                FlightStorage.AddFlight(flight, _context);
            }
                     
            return Created("", flight);          
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlight(int id)
        {
            lock (flightLock)
            {
                FlightStorage.DeleteFlight(id, _context);
            }
            
            return Ok();
        }
    }
}
