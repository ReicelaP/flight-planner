//using FlightPlanner.Validations;
//using Microsoft.AspNetCore.Mvc;

//namespace FlightPlanner.Controllers
//{
//    [Route("api")]
//    [ApiController]
//    public class CustomerApiController : ControllerBase
//    {
//        private readonly FlightPlannerDbContext _context;

//        public CustomerApiController(FlightPlannerDbContext context)
//        {
//            _context = context;
//        }

//        private static readonly object flightLock = new object();

//        [Route("airports")]
//        [HttpGet]
//        public IActionResult GetAirport(string search)
//        {
//            var result = FlightStorage.SearchAirport(search, _context);
//            return Ok(result);
//        }

//        [Route("flights/{id}")]
//        [HttpGet]
//        public IActionResult FindFlightById(int id)
//        {
//            if (FlightStorage.IsExistingFlight(id))
//            {
//                var result = FlightStorage.GetFlight(id, _context);
//                return Ok(result);
//            }
            
//            return NotFound();         
//        }

//        [Route("flights/search")]
//        [HttpPost]
//        public IActionResult FindFlights(SearchFlightsRequest request)              
//        {
//            if (!SearchRequestValidators.IsValidSearch(request) ||
//                !SearchRequestValidators.IsValidDestinationAirport(request))
//            {
//                return BadRequest();
//            }

//            lock (flightLock)
//            {
//                var result = FlightStorage.GetFlightsInfoFromSearch(request, _context);
//                return Ok(result);
//            }         
//        }
//    }
//}
