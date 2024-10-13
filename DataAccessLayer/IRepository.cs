using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// CRUD operations
    /// </summary>
    /// <typeparam name="T">object in database</typeparam>
    public interface IRepository<T> where T : IDomainObject, new()
    {
        void Create(T obj);
        T ReadById(int id);
        IEnumerable<T> ReadAll();
        void Update(T obj);
        void Delete(T obj);
    }
}
