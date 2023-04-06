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
    public sealed class VehicleTypesRepository : IVehicleTypesRepository
    {
        private readonly string _connectionString;
        public VehicleTypesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task Add(VehicleTypes vehicleType)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = @"insert into VehicleTypes (Name, TaxCoefficient) values
                                (
                                    @Name,
                                    @TaxCoefficient
                                )";
                await connection.ExecuteAsync(sqlQuery, vehicleType);
            }
        }

        public async Task CreateTable()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = @"create table VehicleTypes
                                (
                                VehicleTypeId int primary key identity(1,1) not null,
                                Name nvarchar(50) not null,
                                TaxCoefficient float not null
                                )";
                await connection.ExecuteAsync(sqlQuery);
            }
        }

        public async Task Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = "delete from VehicleTypes where VehicleTypeId = @id";
                await connection.ExecuteAsync(sqlQuery, new { id = id });
            }
        }

        public async Task<IEnumerable<VehicleTypes>> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = "select * from VehicleTypes";
                return await connection.QueryAsync<VehicleTypes>(sqlQuery);
            }
        }

        public async Task<VehicleTypes> GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = "select * from VehicleTypes where VehicleTypeId = @id";
                return await connection.QuerySingleAsync<VehicleTypes>(sqlQuery, new { id = id });
            }
        }

        public async Task Update(int id, VehicleTypes vehicleType)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = @"Update VehicleTypes set Name = @Name, TaxCoefficient = @TaxCoefficient where VehicleId = @id";
                await connection.ExecuteAsync(sqlQuery, new
                {
                    Name = vehicleType.Name,
                    TaxCoefficient = vehicleType.TaxCoefficient,
                    id = id
                });
            }
        }
    }
}
