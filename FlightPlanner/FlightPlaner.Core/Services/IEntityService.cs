using FlightPlaner.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlaner.Core.Services
{
    public interface IEntityService<T> where T : Entity
    {
        ServiceResult Create(T entity);
        ServiceResult Delete(T entity);
        ServiceResult Update(T entity);
        List<T> GetAll();
        T GetById(int id);
        IQueryable<T> Query();
    }
}
