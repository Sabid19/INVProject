using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Repository;
using DataAccess;
using POSWeb.ViewModel.Stock;
using System.Data.Entity;
using POSWeb.ViewModel.Report;

namespace POSWeb.Controllers
{
    public class ReportController : Controller
    {
        Repository<Stock> stkrepo = new Repository<Stock>();
        Repository<Sale> salrepo = new Repository<Sale>();
        ASITPOSDBEntities db = new ASITPOSDBEntities();
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
            
            var list = db.Sales.Where(x => DbFunctions.DiffDays(x.Date, getDate) == 0).ToList();
            return PartialView("_DailySalesPartialView", list);
        }

        public ActionResult MonthlySalesByDate()
        {
            int year =Convert.ToInt32( DateTime.Today.ToString("yyyy"));
            int month = Convert.ToInt32(DateTime.Today.ToString("MM"));
            int daysInMonth = DateTime.DaysInMonth(year, month);
            var days = Enumerable.Range(1, daysInMonth);
            var query = db.Sales.Where(x => x.Date.Year == year && x.Date.Month == month).OrderBy(x => x.Date).Select(g => new
            {
                Day = g.Date.Day,
                Total = g.GrandTotal
            });
            var model = new SalesVM
            {
                Date = new DateTime(year, month, 1),
                Days = days.GroupJoin(query, d => d, q => q.Day, (d, q) => new DayTotalVM
                {
                    Day = d,
                    Total = q.Sum(x => x.Total)
                }).ToList()
            };
            return View(model);
        }

        //Returns Monthly sales based on a parameter
        [HttpPost]
        public ActionResult MonthlySalesByDate(string _year, string _month)
        {
            //assign incoming values to the variables
            int year = 0, month = 0;
            //check if year is null
            if (string.IsNullOrWhiteSpace(_year) && _month != null)
            {
                year = DateTime.Now.Date.Year;
                month = Convert.ToInt32(_month.Trim());
            }
            else
            {
                year = Convert.ToInt32(_year.Trim());
                month = Convert.ToInt32(_month.Trim());
            }
            //calculate ttal number of days in a particular month for a that year 
            int daysInMonth = DateTime.DaysInMonth(year, month);
            var days = Enumerable.Range(1, daysInMonth);
            var query = db.Sales.Where(x => x.Date.Year == year && x.Date.Month == month).OrderBy(x => x.Date.Day).Select(g => new
            {
                Day = g.Date.Day,
                Total = g.GrandTotal
            });
            var model = new SalesVM
            {
                Date = new DateTime(year, month, 1),
                Days = days.GroupJoin(query, d => d, q => q.Day, (d, q) => new DayTotalVM
                {
                    Day = d,
                    Total = q.Sum(x => x.Total)
                }).ToList()
            };
            return View(model);
        }

        public ActionResult Purchase()
        {

            return View(db.Purchases.ToList());
        }


        [HttpPost]
        public ActionResult Purchase(PurchaseSearchVM vm)
        {
            var filter = new PurchaseFilter();
            var model = filter.FilterPurchase(vm);
            return View(model.ToList());
        }
        [HttpGet]
        public ActionResult ProfitAndLoss()
        {
            List<Expense> exp = new List<Expense>();
            VmProfitLoss VMP = new VmProfitLoss();
            VMP.expences = exp;
            return View(VMP);
        }

        [HttpPost]
        public ActionResult ProfitAndLoss(DateTime FrmDate, DateTime ToDate)
        {
            ASITPOSDBEntities db = new ASITPOSDBEntities();
            List<ProfitAndLoss> prf = new List<ProfitAndLoss>();
            var pro = db.Profitandloss(FrmDate, ToDate);
            foreach (var item in pro)
            {
                prf.Add(item);
            }
            var exp = db.Expenses.Where(x => x.Date >= FrmDate && x.Date <= ToDate).ToList();

            VmProfitLoss VMP = new VmProfitLoss();
            VMP.Costofgood = prf[0].Costofgood;
            VMP.grossprofit = prf[0].grossprofit;
            VMP.totalsale = prf[0].totalsale;
            VMP.expences= exp;
            return View(VMP);
        }

        [HttpGet]
        public ActionResult YearlySalesByMonth()
        {
            List<MonthTotalVM> _Model = new List<MonthTotalVM>();
            return View(_Model);
        }

        [HttpPost]
        public ActionResult YearlySalesByMonth(string year)
        {
            int _year = 0;
            int _toYear = 0;
            if (string.IsNullOrWhiteSpace(year))
            {
                _year = DateTime.Now.Year;
                _toYear = _year + 1;
            }
            else
            {
                _year = Convert.ToInt32(year);
                _toYear = _year + 1;
            }

            //    int _year = DateTime.Now.Year;
            //    int _toYear = _year + 1;
            var query = db.Sales.Where(s => (s.Date.Year >= _year && s.Date.Year < _toYear));
            // YearlyReportVM _Model = new YearlyReportVM();
            List<MonthTotalVM> _Model = new List<MonthTotalVM>();

            for (int i = 1; i < 13; i++)
            {
                decimal _grandTotal = 0;
                decimal temp = 0;
                temp = query.Where(x => x.Date.Month == i).Sum(x => (decimal?)(x.GrandTotal)) ?? 0;

                _grandTotal = temp;

                MonthTotalVM model = new MonthTotalVM()
                {
                    Year = _year,
                    Month = i,
                    GrandTotal = _grandTotal
                };
                _Model.Add(model);
            }

            return View(_Model.ToList());
        }


        public List<MonthTotalVM> YearlySalesByMonth_forCharts(string year)
        {
            int _year = 0;
            int _toYear = 0;
            if (string.IsNullOrWhiteSpace(year))
            {
                _year = DateTime.Now.Year;
                _toYear = _year + 1;
            }
            else
            {
                _year = Convert.ToInt32(year);
                _toYear = _year + 1;
            }

            var query = db.Sales.Where(s => (s.Date.Year >= _year && s.Date.Year < _toYear));
            List<MonthTotalVM> _Model = new List<MonthTotalVM>();

            for (int i = 1; i < 13; i++)
            {
                decimal _grandTotal = 0;
                decimal temp = 0;
                temp = query.Where(x => x.Date.Month == i).Sum(x => (decimal?)(x.GrandTotal)) ?? 0;

                _grandTotal = temp;

                MonthTotalVM model = new MonthTotalVM()
                {
                    Year = _year,
                    Month = i,
                    GrandTotal = _grandTotal
                };
                _Model.Add(model);
            }
            return _Model.ToList();
        }

    }
}