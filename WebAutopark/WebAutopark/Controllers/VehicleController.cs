using DataLayer.Entities;
using DataLayer.Repositories.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAutopark.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IVehicleTypesRepository _vehicleTypesRepository;
        public VehicleController(IVehicleRepository vehicleRepository, IVehicleTypesRepository vehicleTypesRepository)
        {
            _vehicleRepository = vehicleRepository;
            _vehicleTypesRepository = vehicleTypesRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicle(Vehicles vehicle, string vehicleTypeName)
        {
            var vehicleType = _vehicleTypesRepository.GetAll().Result.First(q => q.Name == vehicleTypeName);
            vehicle.VehicleTypeId = vehicleType.VehicleTypeId;
            await _vehicleRepository.Add(vehicle);
            return RedirectToAction("GetAllVehicles", "Vehicle");
        }

        [HttpGet]
        public IActionResult AddVehicle()
        {
            return View();
        }
        public async Task<IActionResult> DeleteVehicleById(int vehicleId)
        {
            await _vehicleRepository.Delete(vehicleId);
            return RedirectToAction("GetAllVehicles", "Vehicle");
        }
        public async Task<IActionResult> GetAllVehicles()
        {
            var listWithVehicles = await _vehicleRepository.GetAll();
            return View(listWithVehicles);
        }
        [HttpPost]
        public async Task<IActionResult> GetVehicleById(int vehicleId)
        {
            var vehicle = await _vehicleRepository.GetById(vehicleId);
            return View(vehicle);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateVehicle(int vehicleId)
        {
            var vehicle = await _vehicleRepository.GetById(vehicleId);
            return View(vehicle);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateVehicle(Vehicles vehicle, string vehicleTypeName)
        {
            vehicle.VehicleTypeId = _vehicleTypesRepository.GetAll().Result.First(q => q.Name == vehicleTypeName).VehicleTypeId;
            await _vehicleRepository.Update(vehicle.VehicleId, vehicle);
            return RedirectToAction("GetAllVehicles", "Vehicle");
        }
    }
}
