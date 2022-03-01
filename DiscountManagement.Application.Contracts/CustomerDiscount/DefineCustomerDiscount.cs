﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;

namespace DiscountManagement.Application.Contracts.CustomerDiscount
{
    public class DefineCustomerDiscount
    {
        [Range(1, long.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        public long ProductId { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public int DiscountRate { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string StartDate { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string EndDate { get; set; }

        public string Reason { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}