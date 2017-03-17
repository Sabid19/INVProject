using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntityL.Sales
{
   public class SalesInvRpt
    {
        public int ID { get; set; }
        public int StockID { get; set; }
        public string Proname { get; set; }
        public int SalesID { get; set; }
        public int Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
    }
}
