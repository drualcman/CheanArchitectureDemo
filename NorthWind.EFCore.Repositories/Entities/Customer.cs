﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.EFCore.Repositories.Entities
{
    public class Customer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}
