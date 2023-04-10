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
            concreteVehicles.TaxPerMonth = (concreteVehicles.Weight * 0.013) + (taxCoefficient * 30) + 5;
        }
        public static void GetMaxKilometersOnTank(ConcreteVehicles concreteVehicles, double taxCoefficient)
        {
            concreteVehicles.TaxPerMonth = (concreteVehicles.Weight * 0.013) + (taxCoefficient * 30) + 5;
        }

    }
}
