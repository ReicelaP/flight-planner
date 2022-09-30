using FlightPlanner.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            //var flight = FlightStorage.GetFlight(id);
            var flight = _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .FirstOrDefault(f => f.Id == id);

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

                _context.Flights.Add(flight);
                _context.SaveChanges();
                //flight = FlightStorage.AddFlight(flight);
            }
                     
            return Created("", flight);          
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlight(int id)
        {
            lock (flightLock)
            {
                var flight = _context.Flights.FirstOrDefault(f => f.Id == id);
                if (flight != null)
                {
                    _context.Flights.Remove(flight);
                    _context.SaveChanges();
                }

                //FlightStorage.DeleteFlight(id);
            }
            
            return Ok();
        }
    }
}
