using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Botcoin.Models
{
    public class PriceRecord
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(5)]
        public string Coin { get; set; }

        public double Price { get; set; }

        public DateTime DateRegistered { get; set; }
    }
}