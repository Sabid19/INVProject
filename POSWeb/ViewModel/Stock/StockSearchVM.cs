using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;

namespace POSWeb.ViewModel.Stock
{
    public class StockSearchVM
    {
        public List<DataAccess.Stock> stock { get; set; }
        //addded
        public string option { get; set; }
        public string batch { get; set; }
        public string name { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
    }
}