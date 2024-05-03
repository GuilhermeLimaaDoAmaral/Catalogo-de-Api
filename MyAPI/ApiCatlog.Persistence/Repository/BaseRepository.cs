using ApiCatalog.Core.Entities;
using ApiCatalog.Core.Interfaces;
using ApiCatalog.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCatalog.Persistence.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : EntityBase
    {
        private ApplicationDbContext _applicationDbContext { get; set; }
        public BaseRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public virtual IEnumerable<T?> GetAll()
            => _applicationDbContext.Set<T>()
                            .ToList();

        public virtual T? GetById(long id)
            => _applicationDbContext.Set<T>()
                            .Where(x => x.Id == id)
                            .FirstOrDefault();

        public virtual T Create(T obj)
        {
            _applicationDbContext.Add(obj);
            return obj;
        }

        public virtual T Update(T obj)
        {
            _applicationDbContext.Set<T>().Update(obj);
            return obj;
        }

        public virtual void Delete(long id)
        {
            var obj = GetById(id);

            if (obj != null)
            {
                _applicationDbContext.Remove(obj);
            }
        }

    }
}
