using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Botcoin.Models
{
    public class OrderModel
    {
        public long Id { get; set; }

        [Required]
        [MaxLength(5)]
        public string Coin { get; set; }

        public double Amount { get; set; }

        public double Price { get; set; }

        public DateTime DateRegistered { get; set; }
    }
}