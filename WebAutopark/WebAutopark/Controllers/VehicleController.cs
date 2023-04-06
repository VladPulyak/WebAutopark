using DataLayer.Entities;
using DataLayer.Repositories.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using WebAutopark.Mappers;
using WebAutopark.Models;

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
        public async Task<IActionResult> AddVehicle(VehicleViewModel vehicle)
        {
            await _vehicleRepository.Add(VehicleMappers.MapFromVehiclesVMToVehicles(vehicle, await _vehicleTypesRepository.GetAll()));
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
            var vehicleTypes = await _vehicleTypesRepository.GetAll();
            var vehicles = await _vehicleRepository.GetAll();
            var vehiclesViewModels = new List<VehicleViewModel>();
            foreach (var vehicle in vehicles)
            {
                vehiclesViewModels.Add(VehicleMappers.MapFromVehiclesToVehiclesVM(vehicle, vehicleTypes));
            }
            return View(vehiclesViewModels);
        }
        public async Task<IActionResult> GetVehicleById(int vehicleId)
        {
            var vehicleViewModel = VehicleMappers.MapFromVehiclesToVehiclesVM(await _vehicleRepository.GetById(vehicleId), await _vehicleTypesRepository.GetAll());
            return View(vehicleViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateVehicle(int vehicleId)
        {
            var vehicleViewModel = VehicleMappers.MapFromVehiclesToVehiclesVM(await _vehicleRepository.GetById(vehicleId), await _vehicleTypesRepository.GetAll());
            return View(vehicleViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateVehicle(VehicleViewModel vehicleViewModel)
        {
            await _vehicleRepository.Update(vehicleViewModel.VehicleId, VehicleMappers.MapFromVehiclesVMToVehicles(vehicleViewModel, await _vehicleTypesRepository.GetAll()));
            return RedirectToAction("GetAllVehicles", "Vehicle");
        }
    }
}
