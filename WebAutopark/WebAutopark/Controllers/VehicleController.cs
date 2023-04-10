using BusinessLayer.Services;
using DataLayer.Entities;
using DataLayer.Repositories.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task<IActionResult> AddVehicle()
        {
            var listWithVehicleTypes = await _vehicleTypesRepository.GetAll();
            var listWithNameOfTypes = new List<string>();
            foreach (var vehicleType in listWithVehicleTypes)
            {
                listWithNameOfTypes.Add(vehicleType.Name);
            }
            ViewBag.ListWithVehicleTypes = listWithNameOfTypes;
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
            var vehicle = await _vehicleRepository.GetById(vehicleId);
            var vehicleTypes = await _vehicleTypesRepository.GetAll();
            var concreteVehicle = VehicleMappers.MapFromVehicleToConreteVehicle(vehicle, vehicleTypes);
            VehicleService.GetTaxPerMonth(concreteVehicle, vehicleTypes.Single(q => q.VehicleTypeId == vehicle.VehicleTypeId).TaxCoefficient);
            var vehicleGetByIdViewModel = VehicleMappers.MapFromConcreteVehicleToVehiclesGetByIdVM(concreteVehicle, vehicleTypes);
            return View(vehicleGetByIdViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateVehicle(int vehicleId)
        {
            var listWithVehicleTypes = await _vehicleTypesRepository.GetAll();
            var listWithNameOfTypes = new List<string>();
            foreach (var vehicleType in listWithVehicleTypes)
            {
                listWithNameOfTypes.Add(vehicleType.Name);
            }
            ViewBag.ListWithVehicleTypes = listWithNameOfTypes;
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
