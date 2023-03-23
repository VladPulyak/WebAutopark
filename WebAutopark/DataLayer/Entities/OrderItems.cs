﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public sealed class OrderItems
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ComponentId { get; set; }
        public int Quantity { get; set; }
    }
}
