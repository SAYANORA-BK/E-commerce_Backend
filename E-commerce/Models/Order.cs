﻿using System.ComponentModel.DataAnnotations;
using E_commerce.Dto;

namespace E_commerce.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public int AddressId { get; set; }
        [Required]
        public decimal? TotalAmount { get; set; }

        [Required]
        public string? TransactionId { get; set; }



        public virtual User? User { get; set; }
        public virtual Address Address { get; set; }
        public virtual List<OderItems>? OrderItems { get; set; }
        public virtual List<OrderViewDto>? Views { get; set; }
        public string Status { get; set; } = "Pending";
    }
}
