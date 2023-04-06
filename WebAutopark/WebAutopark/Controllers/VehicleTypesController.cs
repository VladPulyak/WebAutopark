using DataLayer.Entities;
using DataLayer.Repositories.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAutopark.Controllers
{
    public class VehicleTypesController : Controller
    {
        private readonly IVehicleTypesRepository _vehicleTypesRepository;
        public VehicleTypesController(IVehicleTypesRepository vehicleTypesRepository)
        {
            _vehicleTypesRepository = vehicleTypesRepository;
        }

        public async Task<IActionResult> AddVehicleType()
        {
            var vehicleType = new VehicleTypes()
            {
                Name = "Car",
                TaxCoefficient = 7.8
            };
            await _vehicleTypesRepository.Add(vehicleType);
            return RedirectToAction("GetAllVehicleTypes", "VehicleTypes");
        }

        public async Task<IActionResult> DeleteVehicleTypeById()
        {
            await _vehicleTypesRepository.Delete(4);
            return RedirectToAction("GetAllVehicleTypes", "VehicleTypes");
        }
        public async Task<IActionResult> GetAllVehicleTypes()
        {
            var listWithVehicles = await _vehicleTypesRepository.GetAll();
            return View(listWithVehicles);
        }
        public async Task<IActionResult> GetVehicleTypesById()
        {
            var vehicle = await _vehicleTypesRepository.GetById(19);
            return View(vehicle);
        }

        public async Task<IActionResult> UpdateVehicleType()
        {
            var vehicleType = new VehicleTypes()
            {
                Name = "Bus",
                TaxCoefficient = 7.8
            };
            await _vehicleTypesRepository.Update(5, vehicleType);
            return RedirectToAction("GetAllVehicleTypes", "VehicleTypes");
        }
    }
}
