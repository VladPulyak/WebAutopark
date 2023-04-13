using DataLayer.Entities;
using WebAutopark.Models;

namespace WebAutopark.Mappers
{
    public static class OrderMappers
    {
        public static Components MapFromOrderItemVMToComponents(OrderItemViewModel orderItemViewModel)
        {
            return new Components
            {
                Name = orderItemViewModel.ComponentName
            };
        }
        public static Orders MapFromOrderItemVMToOrders(OrderItemViewModel orderItemViewModel, int vehicleId, out DateTime currentDate)
        {
            currentDate = DateTime.Now;
            return new Orders
            {
                VehicleId = vehicleId,
                Date = currentDate
            };
        }
        public static OrderItems MapFromOrderItemVMToOrderItem(OrderItemViewModel orderItemViewModel, IEnumerable<Components> components, int orderId)
        {
            var orderItem = new OrderItems()
            {
                OrderId = orderId,
                ComponentId = components.Single(q => q.Name == orderItemViewModel.ComponentName).ComponentId,
            };
            bool isParsedQuantity = int.TryParse(orderItemViewModel.Quantity, out var quantity);
            if (isParsedQuantity)
            {
                orderItem.Quantity = quantity;
            }
            return orderItem;
        }
        public static ComponentViewModel MapFromOrderItemToComponentVM(OrderItems orderItems, Components component)
        {
            var orderItem = new ComponentViewModel()
            {
                ComponentName = component.Name,
                Quantity = orderItems.Quantity.ToString()
            };
            return orderItem;
        }

        public static OrderViewModel MapFromOrderToOrderVM(Orders order, IEnumerable<Vehicles> vehicles)
        {
            var vehicle = vehicles.Single(q => q.VehicleId == order.VehicleId);
            return new OrderViewModel
            {
                OrderId = order.OrderId,
                VehicleModelName = vehicle.Model,
                VehicleRegistrationNumber = vehicle.RegistrationNumber,
                Date = order.Date.ToString()
            };
        }
    }
}
