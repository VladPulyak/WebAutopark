﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public sealed class VehicleTypes
    {
        public int VehicleTypeId { get; set; }
        public string Name { get; set; }
        public double TaxCoefficient { get; set; }
    }
}
