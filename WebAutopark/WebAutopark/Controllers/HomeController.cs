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

        public async Task<IActionResult> Index()
        {
            var vehicle = new Vehicles()
            {
                VehicleTypeId = 1,
                Model = "Citroen C4",
                RegistrationNumber = "1234 AA-5",
                Weight = 2.2,
                Year = DateTime.Now,
                Mileage = 20000,
                Color = Color.White.ToString(),
                FuelConsumption = 7.6
            };
            //await _vehicleRepository.Add(vehicle);
            await _vehicleRepository.Delete(1);
            return View();
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