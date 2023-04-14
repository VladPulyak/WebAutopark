using DataLayer.Entities;
using DataLayer.Repositories;
using DataLayer.Repositories.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAutopark.Mappers;
using WebAutopark.Models;

namespace WebAutopark.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IOrderItemsRepository _orderItemsRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IComponentsRepository _componentsRepository;
        public OrderController(IOrdersRepository ordersRepository, IOrderItemsRepository orderItemsRepository, IVehicleRepository vehicleRepository, IComponentsRepository componentsRepository)
        {
            _ordersRepository = ordersRepository;
            _orderItemsRepository = orderItemsRepository;
            _vehicleRepository = vehicleRepository;
            _componentsRepository = componentsRepository;
        }
        [HttpGet]
        public async Task<IActionResult> AddOrder()
        {
            var components = await _componentsRepository.GetAll();
            var listWithComponentNames = new List<string>();
            foreach (var component in components)
            {
                listWithComponentNames.Add(component.Name);
            }
            ViewBag.ListWithComponents = listWithComponentNames;
            var vehicles = await _vehicleRepository.GetAll();
            ViewBag.ListWithVehicles = vehicles.Select(q => new SelectListItem($"{q.Model} {q.RegistrationNumber}", q.VehicleId.ToString()));
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderItemViewModel orderItemViewModel, int vehicleId)
        {
            var vehicles = await _vehicleRepository.GetAll();
            var order = _ordersRepository.GetAll().Result.SingleOrDefault(q => q.VehicleId == vehicleId);
            if (order is null)
            {
                await _ordersRepository.Add(OrderMappers.MapFromOrderItemVMToOrders(orderItemViewModel, vehicleId, out var currentDate));
            }
            var components = await _componentsRepository.GetAll();
            var orderId = _ordersRepository.GetAll().Result.Single(q => q.VehicleId == vehicleId).OrderId;
            await _orderItemsRepository.Add(OrderMappers.MapFromOrderItemVMToOrderItem(orderItemViewModel, components, orderId));
            return RedirectToAction("GetAllOrders", "Order");
        }
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _ordersRepository.GetAll();
            var vehicles = await _vehicleRepository.GetAll();
            var orderViewModels = new List<OrderViewModel>();
            foreach (var order in orders)
            {
                orderViewModels.Add(OrderMappers.MapFromOrderToOrderVM(order, vehicles));
            }
            return View(orderViewModels);
        }

        public async Task<IActionResult> GetOrderDetails(int orderId)
        {
            var components = await _componentsRepository.GetAll();
            var listWithOrderDetails = await _orderItemsRepository.GetAll();
            var listWithConcreteOrderDetails = listWithOrderDetails.Where(q => q.OrderId == orderId);
            var listWithComponentVM = new List<ComponentViewModel>();
            foreach (var concreteOrderDetail in listWithConcreteOrderDetails)
            {
                listWithComponentVM.Add(OrderMappers.MapFromOrderItemToComponentVM(concreteOrderDetail, await _componentsRepository.GetById(concreteOrderDetail.ComponentId)));
            }
            var order = await _ordersRepository.GetById(orderId);
            ViewBag.Vehicle = await _vehicleRepository.GetById(order.VehicleId);
            return View(listWithComponentVM);
        }
    }
}
