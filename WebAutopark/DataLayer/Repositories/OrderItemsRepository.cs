using Dapper;
using DataLayer.Entities;
using DataLayer.Repositories.RepositoryInterfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataLayer.Repositories
{
    public class OrderItemsRepository : IOrderItemsRepository
    {
        private readonly string _connectionString;
        public OrderItemsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task Add(OrderItems orderItem)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = @"insert into OrderItems (OrderId, ComponentId, Quantity) values
                                (
                                    @OrderId,
                                    @ComponentId,
                                    @Quantity
                                )";
                await connection.ExecuteAsync(sqlQuery, orderItem);
            }
        }
        public async Task CreateTable()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = @"create table OrderItems
                                (
                                OrderItemId int primary key identity(1,1) not null,
                                OrderId int not null,
                                ComponentId int not null,
                                Quantity int not null,
                                constraint FK_OrderId foreign key (OrderId) references Orders(OrderId) on delete cascade,
                                constraint FK_ComponentId foreign key (ComponentId) references Components(ComponentId) on delete cascade
                                )";
                await connection.ExecuteAsync(sqlQuery);
            }
        }

        public async Task Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = "delete from OrderItems where OrderItemId = @id";
                await connection.ExecuteAsync(sqlQuery, new { id = id });
            }
        }

        public async Task<IEnumerable<OrderItems>> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = "select * from OrderItems";
                return await connection.QueryAsync<OrderItems>(sqlQuery);
            }
        }

        public async Task<OrderItems> GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = "select * from OrderItems where OrderItemId = @id";
                return await connection.QuerySingleAsync<OrderItems>(sqlQuery, new { id = id });
            }
        }

        public async Task Update(int id, OrderItems orderItem)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = @"Update OrderItems set OrderId = @OrderId, ComponentId = @ComponentId, Quantity = @Quantity where OrderId = @id";
                await connection.ExecuteAsync(sqlQuery, new
                {
                    OrderId = orderItem.OrderId,
                    ComponentId = orderItem.ComponentId,
                    Quantity = orderItem.Quantity,
                    id = id
                });
            }
        }
    }
}
