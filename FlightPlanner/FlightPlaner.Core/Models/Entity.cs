using FlightPlaner.Core.Interfaces;

namespace FlightPlaner.Core.Models
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
