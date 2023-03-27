using DataLayer.Entities;
using DataLayer.Repositories.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAutopark.Models;

namespace WebAutopark.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVehicleRepository _vehicleRepository;

        public HomeController(ILogger<HomeController> logger, IVehicleRepository vehicleRepository)
        {
            _logger = logger;
            _vehicleRepository = vehicleRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddVehicle()
        {
            var vehicle = new Vehicles()
            {
                VehicleTypeId = 1,
                Model = "LAmborgini Aventador",
                RegistrationNumber = "1234 AA-5",
                Weight = 2.2,
                Year = DateTime.Now,
                Mileage = 20000,
                Color = Color.White.ToString(),
                FuelConsumption = 7.6
            };
            await _vehicleRepository.Add(vehicle);
            return RedirectToAction("GetAllVehicles", "Home");
        }

        public async Task<IActionResult> DeleteVehicleById()
        {
            await _vehicleRepository.Delete(4);
            return RedirectToAction("GetAllVehicles", "Home");
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
            return RedirectToAction("GetAllVehicles", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}