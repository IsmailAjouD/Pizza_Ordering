﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrdering.Models.Dtos
{
    public class ExtraItemDto
    {
        public int ExtraItemId { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
