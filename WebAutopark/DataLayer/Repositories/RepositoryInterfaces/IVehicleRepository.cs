using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.RepositoryInterfaces
{
    public interface IVehicleRepository : IRepository<Vehicles>
    {
        public Task<IEnumerable<Vehicles>> Sort(string field);
    }
}
