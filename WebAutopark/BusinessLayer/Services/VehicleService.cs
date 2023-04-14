using BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public static class VehicleService
    {
        public static void GetTaxPerMonth(ConcreteVehicles concreteVehicles, double taxCoefficient)
        {
            var taxPerMonth = (concreteVehicles.Weight * 0.013) + (taxCoefficient * 30) + 5;
            concreteVehicles.TaxPerMonth = Math.Round(taxPerMonth, 2);
        }
        public static void GetMaxKilometersOnTank(ConcreteVehicles concreteVehicles)
        {
            var maxKilometersOnTank = (concreteVehicles.TankCapacity / concreteVehicles.FuelConsumption) * 100;
            concreteVehicles.MaxKilometersOnTank = Math.Round(maxKilometersOnTank, 2);
        }
    }
}
