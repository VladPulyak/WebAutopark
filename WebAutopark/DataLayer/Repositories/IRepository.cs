using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetById();
        Task<List<T>> GetAll();
        Task Add(T entity);
        Task<T> Update(T entity);
        Task Delete(int id);
    }
}
