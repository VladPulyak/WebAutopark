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
        private readonly string _connectionString = "Server=localhost;Database=WebAutopark;Trusted_Connection=True;Encrypt=False;";
        //public VehicleRepository(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}

        public Task Add(Vehicles vehicle)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = @"insert into Vehicles (VehicleTypeId, Model, RegistrationNumber, Weight, Year, Mileage, Color, FuelConsumption) values
                                (
                                    @VehicleTypeId,
                                    @Model,
                                    @RegistrationNumber,
                                    @Weight,
                                    @Year,
                                    @Mileage,
                                    @Color,
                                    @FuelConsumption
                                )";
                //connection.ExecuteAsync(sqlQuery, new
                //{
                //    VehicleTypeId = vehicle.VehicleTypeId,
                //    Model = vehicle.Model,
                //    RegistrationNumber = vehicle.RegistrationNumber,
                //    Weight = vehicle.Weight,
                //    Year = vehicle.Year,
                //    Mileage = vehicle.Mileage,
                //    Color = vehicle.Color,
                //    FuelConsumption = vehicle.FuelConsumption
                //});
                var list = connection.QueryAsync<Vehicles>(sqlQuery, vehicle).Result.ToList();
            }
            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = "delete from Vehicles where VehicleId = @id";
                var list = connection.QueryAsync<Vehicles>(sqlQuery, new {id = id}).Result.ToList();
            }
            return Task.CompletedTask;
        }

        public Task<List<Vehicles>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Vehicles> GetById()
        {
            throw new NotImplementedException();
        }

        public Task<Vehicles> Update(Vehicles vehicle)
        {
            throw new NotImplementedException();
        }
    }
}
