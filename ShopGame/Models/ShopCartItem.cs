﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopGame.Models
{
    public class ShopCartItem
    {
        public int? IdProcduct { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
    }
}
