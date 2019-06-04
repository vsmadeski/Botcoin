using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using CsvHelper;

namespace Botcoin.Models
{
    public class CsvImportModel
    {
        public CsvImportModel()
        {

        }

        public string Time { get; set; }

        public string Open { get; set; }

        public string High { get; set; }

        public string Low { get; set; }

        public double Close { get; set; }

        public string Vol { get; set; }

        public string Percent { get; set; }

        public string Id { get; set; }

        public string Timestamp { get; set; }

        public string Vol20 { get; set; }

        public string Vol21 { get; set; }
    }
}