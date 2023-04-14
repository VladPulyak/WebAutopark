using Dapper;
using DataLayer.Entities;
using DataLayer.Repositories.RepositoryInterfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly string _connectionString;
        public OrdersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task Add(Orders order)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = @"insert into Orders (VehicleId, Date) values
                                (
                                    @VehicleId,
                                    @Date
                                )";
                await connection.ExecuteAsync(sqlQuery, order);
            }
        }

        public async Task CreateTable()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = @"create table Orders
                                (
                                OrderId int primary key identity(1,1) not null,
                                VehicleId int not null,
                                Date datetime not null,
                                constraint FK_VehicleId foreign key (VehicleId) references Vehicles(VehicleId) on delete cascade
                                )";
                await connection.ExecuteAsync(sqlQuery);
            }
        }

        public async Task Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = "delete from Orders where OrderId = @id";
                await connection.ExecuteAsync(sqlQuery, new { id = id });
            }
        }

        public async Task<IEnumerable<Orders>> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = "select * from Orders";
                return await connection.QueryAsync<Orders>(sqlQuery);
            }
        }

        public async Task<Orders> GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = "select * from Orders where OrderId = @id";
                return await connection.QuerySingleAsync<Orders>(sqlQuery, new { id = id });
            }
        }

        public async Task Update(int id, Orders order)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = @"Update Orders set VehicleId = @VehicleId, Date = @Date where OrderId = @id";
                await connection.ExecuteAsync(sqlQuery, new
                {
                    VehicleId = order.VehicleId,
                    Date = order.Date,
                    id = id
                });
            }
        }
    }
}
