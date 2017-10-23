using System.Collections.Generic;

namespace DAL.Repositories.Base
{
    public interface IBaseRepository<T> where T : class
    {
        IEnumerable<T> GetList();
        T GetItem(int id);
        void Create(T item); 
        void Update(T item); 
        void Delete(int id);
    }
}
