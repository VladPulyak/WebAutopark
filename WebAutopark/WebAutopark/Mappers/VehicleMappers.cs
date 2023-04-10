using BusinessLayer.DTOs;
using DataLayer.Entities;
using WebAutopark.Models;

namespace WebAutopark.Mappers
{
    public static class VehicleMappers
    {
        public static VehicleViewModel MapFromVehiclesToVehiclesVM(Vehicles vehicle, IEnumerable<VehicleTypes> vehicleTypes)
        {
            return new VehicleViewModel
            {
                Color = vehicle.Color,
                FuelConsumption = vehicle.FuelConsumption.ToString(),
                Mileage = vehicle.Mileage.ToString(),
                Model = vehicle.Model,
                RegistrationNumber = vehicle.RegistrationNumber,
                VehicleId = vehicle.VehicleId,
                VehicleTypeId = vehicle.VehicleTypeId,
                Weight = vehicle.Weight.ToString(),
                Year = vehicle.Year.ToString(),
                VehicleTypeName = vehicleTypes.Where(q => q.VehicleTypeId == vehicle.VehicleTypeId).Single().Name,
                TankCapacity = vehicle.TankCapacity.ToString()
            };
        }

        public static Vehicles MapFromVehiclesVMToVehicles(VehicleViewModel vehicleViewModel, IEnumerable<VehicleTypes> vehicleTypes)
        {
            var listWithVehicles = new List<Vehicles>();
            var vehicle = new Vehicles
            {
                Color = vehicleViewModel.Color,
                Model = vehicleViewModel.Model,
                RegistrationNumber = vehicleViewModel.RegistrationNumber,
                VehicleId = vehicleViewModel.VehicleId,
                VehicleTypeId = vehicleTypes.Where(q => q.Name == vehicleViewModel.VehicleTypeName).Single().VehicleTypeId,
            };
            bool isParsedFuelConsumption = double.TryParse(vehicleViewModel.FuelConsumption, out var fuelConsumption);
            bool isParsedWeight = double.TryParse(vehicleViewModel.Weight, out var weight);
            bool isParsedMileage = int.TryParse(vehicleViewModel.Mileage, out var mileage);
            bool isParsedYear = int.TryParse(vehicleViewModel.Year, out var year);
            bool isParsedTankCapacity = int.TryParse(vehicleViewModel.TankCapacity, out var tankCapacity);
            if (isParsedFuelConsumption)
            {
                vehicle.FuelConsumption = fuelConsumption;
            }
            if (isParsedWeight)
            {
                vehicle.Weight = weight;
            }
            if (isParsedMileage)
            {
                vehicle.Mileage = mileage;
            }
            if (isParsedYear)
            {
                vehicle.Year = year;
            }
            if (isParsedTankCapacity)
            {
                vehicle.TankCapacity = tankCapacity;
            }
            listWithVehicles.Add(vehicle);
            return vehicle;
        }

        public static ConcreteVehicles MapFromVehicleToConreteVehicle(Vehicles vehicle, IEnumerable<VehicleTypes> vehicleTypes)
        {
            return new ConcreteVehicles
            {
                Color = vehicle.Color,
                FuelConsumption = vehicle.FuelConsumption,
                Mileage = vehicle.Mileage,
                Model = vehicle.Model,
                RegistrationNumber = vehicle.RegistrationNumber,
                VehicleId = vehicle.VehicleId,
                VehicleTypeId = vehicle.VehicleTypeId,
                Weight = vehicle.Weight,
                Year = vehicle.Year,
                VehicleTypeName = vehicleTypes.Where(q => q.VehicleTypeId == vehicle.VehicleTypeId).Single().Name,
                TankCapacity = vehicle.TankCapacity
            };
        }

        public static Vehicles MapFromConreteVehicleToVehicle(ConcreteVehicles concreteVehicles, IEnumerable<VehicleTypes> vehicleTypes)
        {
            return new Vehicles
            {
                Color = concreteVehicles.Color,
                Model = concreteVehicles.Model,
                RegistrationNumber = concreteVehicles.RegistrationNumber,
                VehicleId = concreteVehicles.VehicleId,
                VehicleTypeId = vehicleTypes.Where(q => q.Name == concreteVehicles.VehicleTypeName).Single().VehicleTypeId,
                FuelConsumption = concreteVehicles.FuelConsumption,
                Mileage = concreteVehicles.Mileage,
                Weight = concreteVehicles.Weight,
                Year = concreteVehicles.Year,
                TankCapacity = concreteVehicles.TankCapacity
            };
        }

        public static VehicleGetByIdViewModel MapFromConcreteVehicleToVehiclesGetByIdVM(ConcreteVehicles concreteVehicles, IEnumerable<VehicleTypes> vehicleTypes)
        {
            return new VehicleGetByIdViewModel
            {
                Color = concreteVehicles.Color,
                FuelConsumption = concreteVehicles.FuelConsumption.ToString(),
                Mileage = concreteVehicles.Mileage.ToString(),
                Model = concreteVehicles.Model,
                RegistrationNumber = concreteVehicles.RegistrationNumber,
                VehicleId = concreteVehicles.VehicleId,
                VehicleTypeId = concreteVehicles.VehicleTypeId,
                Weight = concreteVehicles.Weight.ToString(),
                Year = concreteVehicles.Year.ToString(),
                VehicleTypeName = vehicleTypes.Where(q => q.VehicleTypeId == concreteVehicles.VehicleTypeId).Single().Name,
                TaxPerMonth = concreteVehicles.TaxPerMonth.ToString(),
                MaxKilometersOnTank = concreteVehicles.MaxKilometersOnTank.ToString(),
                TankCapacity = concreteVehicles.TankCapacity.ToString()
            };
        }

        //public static ConcreteVehicles MapFromVehiclesGetByIdVMToConcreteVehicle(VehicleGetByIdViewModel vehicleViewModel, IEnumerable<VehicleTypes> vehicleTypes)
        //{
        //    var listWithVehicles = new List<Vehicles>();
        //    var vehicle = new Vehicles
        //    {
        //        Color = vehicleViewModel.Color,
        //        Model = vehicleViewModel.Model,
        //        RegistrationNumber = vehicleViewModel.RegistrationNumber,
        //        VehicleId = vehicleViewModel.VehicleId,
        //        VehicleTypeId = vehicleTypes.Where(q => q.Name == vehicleViewModel.VehicleTypeName).Single().VehicleTypeId,
        //    };
        //    bool isParsedFuelConsumption = double.TryParse(vehicleViewModel.FuelConsumption, out var fuelConsumption);
        //    bool isParsedWeight = double.TryParse(vehicleViewModel.Weight, out var weight);
        //    bool isParsedMileage = int.TryParse(vehicleViewModel.Mileage, out var mileage);
        //    bool isParsedYear = int.TryParse(vehicleViewModel.Year, out var year);
        //    if (isParsedFuelConsumption)
        //    {
        //        vehicle.FuelConsumption = fuelConsumption;
        //    }
        //    if (isParsedWeight)
        //    {
        //        vehicle.Weight = weight;
        //    }
        //    if (isParsedMileage)
        //    {
        //        vehicle.Mileage = mileage;
        //    }
        //    if (isParsedYear)
        //    {
        //        vehicle.Year = year;
        //    }
        //    listWithVehicles.Add(vehicle);
        //    return vehicle;
        //}
    }
}
