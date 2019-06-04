using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Botcoin.Models;
using Botcoin.Utils.Static;
using CsvHelper;

namespace Botcoin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpPost]
        public ActionResult About(HttpPostedFileBase csvFile)
        {
            HttpPostedFileBase file = Request.Files[0];

            if(file.ContentLength > 0)
            {
                csvFile.SaveAs(FilePaths.CSVPath);
                ImportData(FilePaths.CSVPath);
            }

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private void ImportData(string filepath)
        {
            using (var reader = new StreamReader(filepath))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.Delimiter = ",";
                csv.Read();                                                        // fazer com tudo string
                csv.ReadHeader();
                csv.ValidateHeader<CsvImportModel>();
                var records = csv.GetRecords<CsvImportModel>().ToList();             // 1726 registros
            }
        }
    }
}