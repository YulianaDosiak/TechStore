using System;
using System.Collections.Generic;
using System.Linq;
using TechStore.DAL.Interfaces;

namespace TechStore.DAL
{
    public class GenericDAL<T> : IGenericDAL<T> where T : class
    {
        protected readonly List<T> _data = new List<T>();
        protected int _nextId = 1;

        public virtual T Create(T entity)
        {
            _data.Add(entity);
            return entity;
        }

        public virtual List<T> GetAll()
        {
            return _data.ToList();
        }

        public virtual T GetById(int id)
        {
            var prop = typeof(T).GetProperty("Id") ?? typeof(T).GetProperty(typeof(T).Name + "Id");
            if (prop == null) return null;
            return _data.FirstOrDefault(e => (int)prop.GetValue(e) == id);
        }

        public virtual T Update(T entity)
        {
            var prop = typeof(T).GetProperty("Id") ?? typeof(T).GetProperty(typeof(T).Name + "Id");
            if (prop == null) return entity;

            int id = (int)prop.GetValue(entity);
            var existing = GetById(id);

            if (existing != null)
            {
                _data.Remove(existing);
                _data.Add(entity);
            }

            return entity;
        }

        public virtual bool Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                _data.Remove(entity);
                return true;
            }
            return false;
        }
    }
}
