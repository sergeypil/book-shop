﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace book_shop.ViewModels
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string ProductIsbn { get; set; }
    }
}
