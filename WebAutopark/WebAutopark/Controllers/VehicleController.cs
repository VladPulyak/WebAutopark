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

        public async Task<IActionResult> DeleteVehicleById()
        {
            await _vehicleRepository.Delete(4);
            return RedirectToAction("GetAllVehicles", "Vehicle");
        }
        public async Task<IActionResult> GetAllVehicles()
        {
            var listWithVehicles = await _vehicleRepository.GetAll();
            return View(listWithVehicles);
        }
        public async Task<IActionResult> GetVehicleById()
        {
            var vehicle = await _vehicleRepository.GetById(19);
            return View(vehicle);
        }

        public async Task<IActionResult> UpdateVehicle()
        {
            var vehicle = new Vehicles()
            {
                VehicleTypeId = 1,
                Model = "Porsche Cayeene GTS V8 BiTurbo",
                RegistrationNumber = "1234 AA-5",
                Weight = 2.2,
                Year = DateTime.Now,
                Mileage = 20000,
                Color = Color.White.ToString(),
                FuelConsumption = 7.6
            };
            await _vehicleRepository.Update(5, vehicle);
            return RedirectToAction("GetAllVehicles", "Vehicle");
        }
    }
}
