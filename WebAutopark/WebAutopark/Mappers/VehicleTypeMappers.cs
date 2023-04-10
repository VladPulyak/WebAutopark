using DataLayer.Entities;
using WebAutopark.Models;

namespace WebAutopark.Mappers
{
    public static class VehicleTypeMappers
    {
        public static VehicleTypeViewModel MapFromVehicleTypeToVehicleTypeVM(VehicleTypes vehicleTypes)
        {
            return new VehicleTypeViewModel
            {
                VehicleTypeId = vehicleTypes.VehicleTypeId,
                Name = vehicleTypes.Name,
                TaxCoefficient = vehicleTypes.TaxCoefficient.ToString()
            };
        }

        public static VehicleTypes MapFromVehicleTypeVMToVehicleType(VehicleTypeViewModel vehicleTypeViewModel)
        {
            var vehicleType = new VehicleTypes
            {
                VehicleTypeId = vehicleTypeViewModel.VehicleTypeId,
                Name = vehicleTypeViewModel.Name
            };
            bool isParsedTaxCoefficient = double.TryParse(vehicleTypeViewModel.TaxCoefficient, out var taxCoefficient);
            if (isParsedTaxCoefficient)
            {
                vehicleType.TaxCoefficient = taxCoefficient;
            }
            return vehicleType;
        }
    }
}
