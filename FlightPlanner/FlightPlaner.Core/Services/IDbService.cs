﻿using FlightPlaner.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlaner.Core.Services
{
    public interface IDbService
    {
        ServiceResult Create<T>(T entity) where T : Entity;
        ServiceResult Delete<T>(T entity) where T : Entity;
        ServiceResult Update<T>(T entity) where T : Entity;
        List<T> GetAll<T>() where T : Entity;
        T GetById<T>(int id) where T : Entity;
        IQueryable<T> Query<T>() where T : Entity;
    }
}
