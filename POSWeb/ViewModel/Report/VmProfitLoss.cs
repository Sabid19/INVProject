using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;

namespace POSWeb.ViewModel.Report
{
    public class VmProfitLoss
    {
        public decimal Costofgood { get; set; }
        public decimal grossprofit { get; set; }
        public decimal totalsale { get; set; }
        public List<Expense> expences { get; set; }
    }
}