using AutoMapper;
using FlightPlaner.Core.Models;
using FlightPlaner.Core.Services;
using FlightPlaner.Core.Validations;
using FlightPlanner.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [ApiController, Authorize]
    public class AdminApiController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IEnumerable<IFlightValidator> _flightValidators;
        private readonly IEnumerable<IAirportValidator> _airportValidators;
        private readonly IMapper _mapper;

        public AdminApiController(IFlightService flightService, 
            IEnumerable<IFlightValidator> flightValidators,
            IEnumerable<IAirportValidator> airportValidators,
            IMapper mapper)
        {
            _flightService = flightService;
            _flightValidators = flightValidators;
            _airportValidators = airportValidators;
            _mapper = mapper;
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            var flight = _flightService.GetCompleteFlightById(id);

            if(flight == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<FlightRequest>(flight);
            return Ok(response);
        }

        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlight(FlightRequest request)
        {
            var flight = _mapper.Map<Flight>(request);

            if (!_flightValidators.All(f => f.IsValid(flight)) ||
                !_airportValidators.All(f => f.IsValid(flight?.From)) ||
                !_airportValidators.All(f => f.IsValid(flight?.To)))
            {
                return BadRequest();
            }

            if (_flightService.Exists(flight))
            {
                return Conflict();
            }

            var result = _flightService.Create(flight);
            if (result.Success)
            {
                request = _mapper.Map<FlightRequest>(flight);
                return Created("", request);
            }

            return Problem(result.ErrorMessage);     
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlight(int id)
        {
            var flight = _flightService.GetById(id);

            if (flight != null)
            {
                var result = _flightService.Delete(flight);
                if (result.Success)
                {
                    return Ok();
                }

                return Problem(result.ErrorMessage);
            }
            
            return Ok();
        }
    }
}
