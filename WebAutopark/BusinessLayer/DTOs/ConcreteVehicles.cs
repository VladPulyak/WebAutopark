using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs
{
    public class ConcreteVehicles
    {
        public int VehicleId { get; set; }
        public int VehicleTypeId { get; set; }
        public string Model { get; set; }
        public string? RegistrationNumber { get; set; }
        public double Weight { get; set; }
        public DateTime Year { get; set; }
        public int Mileage { get; set; }
        public string Color { get; set; }
        public double FuelConsumption { get; set; }
        public double TaxPerMonth { get; set; }
        public int MyProperty { get; set; }
    }
}
