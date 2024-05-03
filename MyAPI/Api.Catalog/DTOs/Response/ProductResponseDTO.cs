﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCatalog.Core.DTOs.Response
{
    public class ProductResponseDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; }
        public string? ImageUrl { get; set; }
        public float Stock { get; set; }
        public DateTime DateRegister { get; set; }
    }
}
