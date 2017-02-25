using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Repository;
using DataAccess;
using POSWeb.ViewModel.Stock;

namespace POSWeb.Controllers
{
    public class ReportController : Controller
    {
        Repository<Stock> stkrepo = new Repository<Stock>();
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
    }
}