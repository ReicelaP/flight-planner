using FlightPlaner.Core.Interfaces;
using System.Collections.Generic;

namespace FlightPlaner.Core.Services
{
    public class ServiceResult
    {
        public ServiceResult(bool success)
        {
            Success = success;
        }

        public ServiceResult SetEntity(IEntity entity)
        {
            Entity = entity;
            return this;
        }

        public ServiceResult AddError(string error)
        {
            Errors.Add(error);
            return this;
        }

        public bool Success { get; private set; }   
        
        public IEntity Entity { get; set; }

        public IList<string> Errors { get; private set; }

        public string ErrorMessage => string.Join(",", Errors);
    }
}
