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
                Year = vehicle.Year,
                VehicleTypeName = vehicleTypes.Where(q => q.VehicleTypeId == vehicle.VehicleTypeId).Single().Name
            };
        }


        //public static IEnumerable<VehicleViewModel> MapFromVehiclesToVehiclesVM(IEnumerable<Vehicles> listWithVehicles, IEnumerable<VehicleTypes> vehicleTypes)
        //{
        //    var listWithVehiclesViewModels = new List<VehicleViewModel>();
        //    foreach (var vehicle in listWithVehicles)
        //    {
        //        listWithVehiclesViewModels.Add(new VehicleViewModel
        //        {
        //            Color = vehicle.Color,
        //            FuelConsumption = vehicle.FuelConsumption.ToString(),
        //            Mileage = vehicle.Mileage,
        //            Model = vehicle.Model,
        //            RegistrationNumber = vehicle.RegistrationNumber,
        //            VehicleId = vehicle.VehicleId,
        //            VehicleTypeId = vehicle.VehicleTypeId,
        //            Weight = vehicle.Weight.ToString(),
        //            Year = vehicle.Year,
        //            VehicleTypeName = vehicleTypes.Where(q => q.VehicleTypeId == vehicle.VehicleTypeId).Single().Name
        //        });
        //    }
        //    return listWithVehiclesViewModels;
        //}



        public static Vehicles MapFromVehiclesVMToVehicles(VehicleViewModel vehicleViewModel, IEnumerable<VehicleTypes> vehicleTypes)
        {
            var listWithVehicles = new List<Vehicles>();
            var vehicle = new Vehicles
            {
                Color = vehicleViewModel.Color,
                Model = vehicleViewModel.Model,
                RegistrationNumber = vehicleViewModel.RegistrationNumber,
                VehicleId = vehicleViewModel.VehicleId,
                Year = vehicleViewModel.Year,
                VehicleTypeId = vehicleTypes.Where(q => q.Name == vehicleViewModel.VehicleTypeName).Single().VehicleTypeId,
            };
            bool isParsedFuelConsumption = double.TryParse(vehicleViewModel.FuelConsumption, out var fuelConsumption);
            bool isParsedWeight = double.TryParse(vehicleViewModel.Weight, out var weight);
            bool isParsedMileage = int.TryParse(vehicleViewModel.Mileage, out var mileage);
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
            listWithVehicles.Add(vehicle);
            return vehicle;
        }
        //public static IEnumerable<Vehicles> MapFromVehiclesVMToVehicles(List<VehicleViewModel> listWithVehiclesVM)
        //{
        //    var listWithVehicles = new List<Vehicles>();
        //    foreach (var vehicleVM in listWithVehiclesVM)
        //    {
        //        var vehicle = new Vehicles
        //        {
        //            Color = vehicleVM.Color,
        //            Mileage = vehicleVM.Mileage,
        //            Model = vehicleVM.Model,
        //            RegistrationNumber = vehicleVM.RegistrationNumber,
        //            VehicleId = vehicleVM.VehicleId,
        //            VehicleTypeId = vehicleVM.VehicleTypeId,
        //            Year = vehicleVM.Year
        //        };
        //        bool isParsedFuelConsumption = double.TryParse(vehicleVM.FuelConsumption, out var fuelConsumption);
        //        bool isParsedWeight = double.TryParse(vehicleVM.FuelConsumption, out var weight);
        //        if (isParsedFuelConsumption)
        //        {
        //            vehicle.FuelConsumption = fuelConsumption;
        //        }
        //        if (isParsedWeight)
        //        {
        //            vehicle.Weight = weight;
        //        }
        //        listWithVehicles.Add(vehicle);
        //    }
        //    return listWithVehicles;
        //}
    }
}
