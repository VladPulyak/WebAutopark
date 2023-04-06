using DataLayer.Entities;
using DataLayer.Repositories;
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
        [HttpGet]
        public IActionResult AddVehicleType()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddVehicleType(VehicleTypes vehicleType)
        {
            await _vehicleTypesRepository.Add(vehicleType);
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
            return View(listWithVehicleTypes);
        }
        [HttpPost]
        public async Task<IActionResult> GetVehicleTypeById(int vehicleTypeId)
        {
            var vehicleType = await _vehicleTypesRepository.GetById(vehicleTypeId);
            return View(vehicleType);
        }
    }
}
