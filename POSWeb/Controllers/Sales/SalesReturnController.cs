using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Repository;
using DataAccess;
using System.Net;

namespace POSWeb.Controllers.Sales
{
    public class SalesReturnController : Controller
    {
        Repository<SalesReturn> salRetRepo = new Repository<SalesReturn>();
        Repository<SalesReturnDetail> RetDetailRepo = new Repository<SalesReturnDetail>();
        // GET: SalesReturn
        public ActionResult Index()
        {
            return View(salRetRepo.GetAll().ToList());
        }

        //POST: SalesReturn/ReturnDetails/5
        public ActionResult ReturnDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = RetDetailRepo.GetAll().Where(s => s.SalesReturnID == id).ToList();
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }


        // POST: SalesReturn/5
        public ActionResult Returns(int id)
        {
            Repository<Sale> salRepo = new Repository<Sale>();
            var model = salRepo.GetById(id);
            //null check
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View(model);
        }


        public ActionResult ReturnItems(FormCollection coll)
        {

            List<SalesReturnDetail> details = new List<SalesReturnDetail>();

            var counter = Convert.ToInt32(coll["counter"]);

            //attributes required for SalesReturn
            decimal total = Convert.ToDecimal(coll["SubTotal"]);
            decimal discount = Convert.ToDecimal(coll["Discount"]);
            decimal netTotal = Convert.ToDecimal(coll["NetTotal"]);
            int salesID = Convert.ToInt32(coll["SalesID"]);

            //populating through each of the occurance of the ReturnedItems
            for (int i = 1; i <= counter; i++)
            {
                var value = coll["Qty_" + i];
                if (!string.IsNullOrEmpty(value) && value != "0")
                {
                    SalesReturnDetail srd = new SalesReturnDetail
                    {
                        StockID = Convert.ToInt32(coll["StockID_" + i]),
                        BatchNo = coll["BatchNo_" + i],
                        Qty = Convert.ToInt32(coll["Qty_" + i]),
                        Rate = Convert.ToDecimal(coll["Rate_" + i]),
                        Amount = Convert.ToDecimal(coll["Amount_" + i])
                    };
                    details.Add(srd);
                }
            }

            //populating Sales Return
            SalesReturn _SalesReturn = new SalesReturn
            {
                SalesID = salesID,
                Subtotal = total,
                Discount = discount,
                NetTotal = netTotal,
                SalesReturnDetails = details,
                Description = "n/a",
                ReturnedDate = DateTime.Today
            };

            //Add in Sales And save changes 
            salRetRepo.Add(_SalesReturn);
           // db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}