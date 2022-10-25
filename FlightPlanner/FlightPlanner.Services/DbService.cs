using FlightPlaner.Core.Models;
using FlightPlaner.Core.Services;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlanner.Services
{
    public class DbService : IDbService
    {
        protected IFlightPlannerDbContext _context;

        public DbService(IFlightPlannerDbContext context)
        {
            _context = context;
        }

        public ServiceResult Create<T>(T entity) where T : Entity
        {
            try
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();

                return new ServiceResult(true).SetEntity(entity);
            }
            catch (Exception e)
            {
                return new ServiceResult(false).AddError(e.Message);
            }           
        }

        public ServiceResult Delete<T>(T entity) where T : Entity
        {
            try
            {
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();

                return new ServiceResult(true);
            }
            catch (Exception e)
            {
                return new ServiceResult(false).AddError(e.Message);
            }        
        }

        public ServiceResult Update<T>(T entity) where T : Entity
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();

                return new ServiceResult(true).SetEntity(entity);
            }
            catch (Exception e)
            {
                return new ServiceResult(false).AddError(e.Message);
            }        
        }

        public List<T> GetAll<T>() where T : Entity
        {
            return _context.Set<T>().ToList();
        }

        public T GetById<T>(int id) where T : Entity
        {
            return _context.Set<T>().SingleOrDefault(e => e.Id == id);
        }

        public IQueryable<T> Query<T>() where T : Entity
        {
            return _context.Set<T>().AsQueryable();
        }
    }
}
