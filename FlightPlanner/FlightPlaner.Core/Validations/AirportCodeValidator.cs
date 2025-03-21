﻿using FlightPlaner.Core.Models;

namespace FlightPlaner.Core.Validations
{
    public class AirportCodeValidator : IAirportValidator
    {
        public bool IsValid(Airport airport)
        {
            return !string.IsNullOrEmpty(airport?.AirportCode);
        }
    }
}
