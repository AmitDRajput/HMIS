﻿using HMIS.DataAccess.Context;
using HMIS.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.DataAccess.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public readonly HMISDbContext _context;
        public GenericRepository(HMISDbContext context)
        {
            _context = context;
        }

        public HMISDbContext Context { get; }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public IEnumerable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await _context.Set<T>().Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .ToListAsync();
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}