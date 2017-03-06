using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSWeb.ViewModel.Report
{
    public class SalesVM
    {
        public DateTime Date { get; set; }
        public List<DayTotalVM> Days { get; set; }
    }
}