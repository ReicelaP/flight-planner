using AutoMapper;
using FlightPlaner.Core.Models;
using FlightPlanner.Models;

namespace FlightPlanner
{
    public class AutoMapperConfig
    {
        public static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AirportRequest, Airport>()
                    .ForMember(d => d.Id, opt => opt.Ignore())
                    .ForMember(d => d.AirportCode, opt => opt.MapFrom(s => s.Airport));

                cfg.CreateMap<Airport, AirportRequest>()
                    .ForMember(d => d.Airport, opt => opt.MapFrom(s => s.AirportCode));
                
                cfg.CreateMap<FlightRequest, Flight>();
                cfg.CreateMap<Flight, FlightRequest>();
            });

            config.AssertConfigurationIsValid();

            return config.CreateMapper();
        }
    }
}
