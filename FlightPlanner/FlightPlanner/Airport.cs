﻿using System.Text.Json.Serialization;

namespace FlightPlanner
{
    public class Airport
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonPropertyName("airport")]
        public string AirportCode { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}
