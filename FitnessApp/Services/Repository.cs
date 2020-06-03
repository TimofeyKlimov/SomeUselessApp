using FitnessApp.Bus;
using FitnessApp.Events;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.Services
{
    public class Repository
    {
       
        private readonly LiteDatabase database;

        private ILiteCollection<T> GetCollection<T>()
        {
            return database.GetCollection<T>();
        }
        public Repository(LiteDatabase database)
        {
            this.database = database;
        }

        public void Save<T>(T entity)
        {
            
            this.GetCollection<T>().Upsert(entity);
        }

        public T FindEntity<T>(Expression<Func<T,bool>> expression)
        {
            var getCol = GetCollection<T>();
            var entity = getCol.Find(expression);
            return entity.FirstOrDefault();
        }
    }
}
