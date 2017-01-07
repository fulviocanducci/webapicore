using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAngularCore.Models
{
    public interface IRepository<T> : IDisposable
    {   
        T Add(T model);
        bool Edit(T model);
        bool Remove(T model);
        T Find(params object[] id);
        IEnumerable<T> All();
        int Save();
    }

    public interface IReferenceRepository : IRepository<Reference> { }

    public class ReferenceRepository : IReferenceRepository
    {
        private Database _database;
        public ReferenceRepository(Database database)
        {
            _database = database;
        }
        public Reference Add(Reference model)
        {
            _database.References.Add(model);
            _database.SaveChanges();
            return model;
        }

        public IEnumerable<Reference> All()
        {
            return _database.References;
        }

        public void Dispose()
        {
            _database.Dispose();
        }

        public bool Edit(Reference model)
        {
            _database.Entry(model).State = EntityState.Modified;
            return _database.SaveChanges() > 0;
        }

        public Reference Find(params object[] id)
        {
            return _database.References.Find(id);
        }

        public bool Remove(Reference model)
        {
            _database.Entry(model).State = EntityState.Deleted;
            return _database.SaveChanges() > 0;
        }

        public int Save()
        {
            return _database.SaveChanges();
        }
    }
}
