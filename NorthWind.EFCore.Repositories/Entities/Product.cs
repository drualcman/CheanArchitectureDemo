﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.EFCore.Repositories.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UnitsInStock { get; set; }
        public decimal UnitPrice { get; set; }
        public bool Discontinued { get; set; }
    }
}
