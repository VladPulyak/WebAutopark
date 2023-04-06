using DataLayer.Entities;
using DataLayer.Repositories.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAutopark.Models;

namespace WebAutopark.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IOrderItemsRepository _orderItemsRepository;
        private readonly IComponentsRepository _componentsRepository;
        private readonly IVehicleTypesRepository _vehicleTypesRepository;

        public HomeController(IVehicleRepository vehicleRepository, IOrdersRepository ordersRepository, IOrderItemsRepository orderItemsRepository, IComponentsRepository componentsRepository, IVehicleTypesRepository vehicleTypesRepository)
        {
            _vehicleRepository = vehicleRepository;
            _ordersRepository = ordersRepository;
            _componentsRepository = componentsRepository;
            _vehicleTypesRepository = vehicleTypesRepository;
            _orderItemsRepository = orderItemsRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateTables()
        {
            await _vehicleTypesRepository.CreateTable();
            await _vehicleRepository.CreateTable();
            await _componentsRepository.CreateTable();
            await _ordersRepository.CreateTable();
            await _orderItemsRepository.CreateTable();
            return RedirectToAction("Index", "Home");
        }
    }
}