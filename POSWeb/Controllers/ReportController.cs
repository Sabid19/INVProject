using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Repository;
using DataAccess;
using POSWeb.ViewModel.Stock;
using System.Data.Entity;

namespace POSWeb.Controllers
{
    public class ReportController : Controller
    {
        Repository<Stock> stkrepo = new Repository<Stock>();
        Repository<Sale> salrepo = new Repository<Sale>();
        // GET: Report
        public ActionResult Index()
        {
          var stok=  stkrepo.GetAll();
            return View(stok);
        }

        [HttpPost]
        public ActionResult Index(StockSearchVM vm)
        {
            var filter = new StockFilter();
            var model = filter.FilterStocks(vm);
            return View(model);
        }

        public ActionResult DailySales()
        {
          //  var list = salrepo.GetAll().Where(x => DbFunctions.DiffDays(x.Date, DateTime.Now) == 0).ToList();
            var list = salrepo.GetAll().ToList();
            return View(list);
        }

        public ActionResult DailySalesFor(DateTime getDate)
        {
            var list = salrepo.GetAll().Where(x => DbFunctions.DiffDays(x.Date, getDate) == 0).ToList();
            return PartialView("_DailySalesPartialView", list);
        }

    }
}