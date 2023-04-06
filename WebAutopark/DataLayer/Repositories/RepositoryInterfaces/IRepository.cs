using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.RepositoryInterfaces
{
    public interface IRepository<T> where T : class
    {
        Task CreateTable();
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        Task Update(int id, T entity);
        Task Delete(int id);
    }
}
