using DataLayer.Entities;
using DataLayer.Repositories;
using DataLayer.Repositories.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using WebAutopark.Mappers;
using WebAutopark.Models;

namespace WebAutopark.Controllers
{
    public class VehicleTypesController : Controller
    {
        private readonly IVehicleTypesRepository _vehicleTypesRepository;
        public VehicleTypesController(IVehicleTypesRepository vehicleTypesRepository)
        {
            _vehicleTypesRepository = vehicleTypesRepository;
        }
        [HttpGet]
        public IActionResult AddVehicleType()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddVehicleType(VehicleTypeViewModel vehicleTypeViewModel)
        {
            await _vehicleTypesRepository.Add(VehicleTypeMappers.MapFromVehicleTypeVMToVehicleType(vehicleTypeViewModel));
            return RedirectToAction("GetAllVehicleTypes", "VehicleTypes");
        }

        public async Task<IActionResult> DeleteVehicleTypeById(int vehicleTypeId)
        {
            await _vehicleTypesRepository.Delete(vehicleTypeId);
            return RedirectToAction("GetAllVehicleTypes", "VehicleTypes");
        }
        public async Task<IActionResult> GetAllVehicleTypes()
        {
            var listWithVehicleTypes = await _vehicleTypesRepository.GetAll();
            var listWithVehicleTypeVM = new List<VehicleTypeViewModel>();
            foreach (var vehicleType in listWithVehicleTypes)
            {
                listWithVehicleTypeVM.Add(VehicleTypeMappers.MapFromVehicleTypeToVehicleTypeVM(vehicleType));
            }
            return View(listWithVehicleTypeVM);
        }
    }
}
