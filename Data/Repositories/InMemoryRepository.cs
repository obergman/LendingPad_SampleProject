using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;

namespace Data.Repositories
{

    public class InMemoryRepository<T> : IRepository<T> where T : IdObject
    {
        protected readonly Dictionary<Guid, T> _context;        

        public InMemoryRepository()
        {
            _context = new Dictionary<Guid, T>();
        }


        public void Add(Guid id, T entity)
        {
            _context.Add(id, entity);
        }

        public void Save(T entity)
        {
            Add(entity.Id, entity);
        }

        public void Delete(T entity)
        {
            _context.Remove(entity.Id);

        }

        public T Get(Guid id)
        {
            return _context.TryGetValue(id, out T result) ? result : null;                       
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Values.AsEnumerable<T>();
        }
    }
}