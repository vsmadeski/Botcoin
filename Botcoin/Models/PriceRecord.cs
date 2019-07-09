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

        public decimal Price { get; set; }

        public DateTime DateRegistered { get; set; }
    }
}