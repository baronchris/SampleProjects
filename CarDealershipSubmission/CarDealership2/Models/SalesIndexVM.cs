﻿using CarDealership.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealership2.Models
{
    public class SalesIndexVM
    {
        public Promotion CurrentPromo { get; set; }
        public List<CarDetailed>Inventory { get; set; }
    }
}