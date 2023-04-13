using Dapper;
using DataLayer.Entities;
using DataLayer.Repositories.RepositoryInterfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public sealed class VehicleRepository : IVehicleRepository
    {
        private readonly string _connectionString;
        public VehicleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task Add(Vehicles vehicle)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = @"insert into Vehicles (VehicleTypeId,VehicleTypeName, Model, RegistrationNumber, Weight, Year, Mileage, Color, FuelConsumption, TankCapacity) values
                                (
                                    @VehicleTypeId,
                                    @VehicleTypeName,                                    
                                    @Model,
                                    @RegistrationNumber,
                                    @Weight,
                                    @Year,
                                    @Mileage,
                                    @Color,
                                    @FuelConsumption,
                                    @TankCapacity
                                )";
                await connection.ExecuteAsync(sqlQuery, vehicle);
            }
        }

        public async Task<IEnumerable<Vehicles>> Sort(string fieldName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"select * from Vehicles order by {fieldName}";
                
                return await connection.QueryAsync<Vehicles>(sqlQuery);
            }
        }

        public async Task CreateTable()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = @"create table Vehicles
                                (
                                VehicleId int primary key identity(1,1) not null,
                                VehicleTypeId int not null,
                                VehicleTypeName nvarchar(50) not null,
                                Model nvarchar(50) not null,
                                RegistrationNumber nvarchar(50) null,
                                Weight float not null,
                                Year int not null,
                                Mileage int not null,
                                Color nvarchar(50) not null,
                                FuelConsumption float not null,
                                TankCapacity int not null,
                                constraint FK_VehicleTypeId foreign key (VehicleTypeId) references VehicleTypes(VehicleTypeId)
                                )";
                await connection.ExecuteAsync(sqlQuery);
            }
        }

        public async Task Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = "delete from Vehicles where VehicleId = @id";
                await connection.ExecuteAsync(sqlQuery, new { id = id });
            }
        }

        public async Task<IEnumerable<Vehicles>> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = "select * from Vehicles";
                return await connection.QueryAsync<Vehicles>(sqlQuery);
            }
        }

        public async Task<Vehicles> GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = "select * from Vehicles where VehicleId = @id";
                return await connection.QuerySingleAsync<Vehicles>(sqlQuery, new { id = id });
            }
        }

        public async Task Update(int id, Vehicles vehicle)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = @"Update Vehicles set Color = @Color,FuelConsumption = @FuelConsumption,
                                                     Mileage = @Mileage, Model = @Model, RegistrationNumber = @RegistrationNumber,
                                                     VehicleTypeId = @VehicleTypeId, Weight = @Weight, Year = @Year, TankCapacity =                      @TankCapacity, VehicleTypeName = @VehicleTypeName
                                                     where VehicleId = @id";
                await connection.ExecuteAsync(sqlQuery, new
                {
                    Color = vehicle.Color,
                    FuelConsumption = vehicle.FuelConsumption,
                    Mileage = vehicle.Mileage,
                    Model = vehicle.Model,
                    RegistrationNumber = vehicle.RegistrationNumber,
                    VehicleTypeId = vehicle.VehicleTypeId,
                    Weight = vehicle.Weight,
                    Year = vehicle.Year,
                    TankCapacity = vehicle.TankCapacity,
                    VehicleTypeName = vehicle.VehicleTypeName,
                    id = id
                });
            }
        }
    }
}
