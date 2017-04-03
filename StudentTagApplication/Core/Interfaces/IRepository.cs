using NPoco;
using System.Collections.Generic;

namespace UniSA.UserTagger.Core.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        List<T> List(string sql = null);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        T FindById(int Id);
    }
}