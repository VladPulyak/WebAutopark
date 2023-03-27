using Dapper;
using DataLayer.Entities;
using DataLayer.Repositories.RepositoryInterfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public sealed class ComponentsRepository : IComponentsRepository
    {
        private readonly string _connectionString;
        public ComponentsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task Add(Components component)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = @"insert into Components (Name) values
                                (
                                    @Name
                                )";
                await connection.ExecuteAsync(sqlQuery, component);
            }
        }

        public async Task Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = "delete from Components where ComponentId = @id";
                await connection.ExecuteAsync(sqlQuery, new { id = id });
            }
        }

        public async Task<IEnumerable<Components>> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = "select * from Components";
                return await connection.QueryAsync<Components>(sqlQuery);
            }
        }

        public async Task<Components> GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = "select * from Components where ComponentId = @id";
                return await connection.QuerySingleAsync<Components>(sqlQuery, new { id = id });
            }
        }

        public async Task Update(int id, Components component)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = @"Update Components set Name = @Name where ComponentId = @id";
                await connection.ExecuteAsync(sqlQuery, new
                {
                    Name = component.Name,
                    id = id
                });
            }
        }
    }
}
