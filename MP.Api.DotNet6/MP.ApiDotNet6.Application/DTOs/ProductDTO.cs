﻿
using MP.ApiDotNet6.Domain.Entities;

namespace MP.ApiDotNet6.Application.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CodErp { get; set; }
        public decimal Price { get; set; }
    }
}
