using AutoMapper;
using FlightPlaner.Core.Models;
using FlightPlaner.Core.Services;
using FlightPlaner.Core.Validations;
using FlightPlanner.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private readonly IAirportService _airportService;
        private readonly IFlightService _flightService;
        private readonly IEnumerable<ISearchValidator> _searchValidators;
        private readonly IMapper _mapper;

        public CustomerApiController(IAirportService airportService, 
            IFlightService flightService,
            IEnumerable<ISearchValidator> searchValidators,
            IMapper mapper)
        {
            _airportService = airportService;
            _flightService = flightService;
            _searchValidators = searchValidators;
            _mapper = mapper;
        }

        [Route("airports")]
        [HttpGet]
        public IActionResult GetAirport(string search)
        {
            var airports = _airportService.SearchAirport(search);
            var airportsRequest = new List<AirportRequest>();

            foreach (Airport airport in airports)
            {
                var response = _mapper.Map<AirportRequest>(airport);
                airportsRequest.Add(response);
            }

            return Ok(airportsRequest.ToArray());
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult FindFlightById(int id)
        {
            var flight = _flightService.GetCompleteFlightById(id);

            if (flight == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<FlightRequest>(flight);

            return Ok(response);
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult FindFlights(SearchFlightsRequest request)
        {
            if (!_searchValidators.All(s => s.IsValid(request)))
            {
                return BadRequest();
            }

            var result = _flightService.GetFlightsInfoFromSearch(request);

            return Ok(result);
        }
    }
}
