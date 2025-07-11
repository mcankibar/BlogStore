﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogStore.DataAccessLayer.Abstract;
using BlogStore.DataAccessLayer.Context;

namespace BlogStore.DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class, new()
    {

        private readonly BlogContext _context;

        public GenericRepository(BlogContext context)
        {
            _context = context;
        }

        public void Insert(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();

        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var value = _context.Set<T>().Find(id);
            _context.Set<T>().Remove(value);
            _context.SaveChanges();

        }

        public List<T> GetAll()
        {
            var values = _context.Set<T>().ToList();
            return values;

        }

        public T GetById(int id)
        {
            var value = _context.Set<T>().Find(id);
            return value;
        }
    }
}
