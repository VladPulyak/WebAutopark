using BusinessLayer.Services;
using DataLayer.Entities;
using DataLayer.Repositories.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;
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
            var vehicles = await _vehicleRepository.GetAll();
            var vehiclesViewModels = new List<VehicleViewModel>();
            foreach (var vehicle in vehicles)
            {
                vehiclesViewModels.Add(VehicleMappers.MapFromVehiclesToVehiclesVM(vehicle));
            }
            return View(vehiclesViewModels);
        }
        public async Task<IActionResult> Sort(string fieldName)
        {
            var listWithVehicles = await _vehicleRepository.Sort(fieldName);
            var listWithVehiclesVM = new List<VehicleViewModel>();
            foreach (var vehicle in listWithVehicles)
            {
                listWithVehiclesVM.Add(VehicleMappers.MapFromVehiclesToVehiclesVM(vehicle));
            }
            return View("GetAllVehicles", listWithVehiclesVM);
        }
        public async Task<IActionResult> GetVehicleById(int vehicleId)
        {
            var vehicle = await _vehicleRepository.GetById(vehicleId);
            var vehicleTypes = await _vehicleTypesRepository.GetAll();
            var concreteVehicle = VehicleMappers.MapFromVehicleToConreteVehicle(vehicle);
            VehicleService.GetTaxPerMonth(concreteVehicle, vehicleTypes.Single(q => q.VehicleTypeId == vehicle.VehicleTypeId).TaxCoefficient);
            VehicleService.GetMaxKilometersOnTank(concreteVehicle);
            var vehicleGetByIdViewModel = VehicleMappers.MapFromConcreteVehicleToVehiclesGetByIdVM(concreteVehicle);
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
            var vehicleViewModel = VehicleMappers.MapFromVehiclesToVehiclesVM(await _vehicleRepository.GetById(vehicleId));
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
