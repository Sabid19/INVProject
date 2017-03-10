using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Repository;
using DataAccess;

namespace POSWeb.Controllers
{
    public class ExpenseController : Controller
    {
        Repository<Expense> exprepo = new Repository<Expense>();
        // GET: Expense
        public ActionResult Index()
        {         
            var exolst = exprepo.GetAll().ToList();
            return View(exolst);
        }

        [HttpGet]
        public  ActionResult Create()
        {
            return View();
        }   

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult Create(Expense exp)
        {
            if (ModelState.IsValid)
            {
                exprepo.Add(exp);
                return RedirectToAction("Index");
            }
            return View(exp);
        }

        public ActionResult Edit(int id)
        {
            var exp = exprepo.GetById(id);
            return View(exp);
        }
        [HttpPost]
        public ActionResult Edit(Expense expn)
        {
            if (ModelState.IsValid)
            {
                var org = exprepo.GetById(expn.ID);
                exprepo.Update(org, expn);

                return RedirectToAction("Index");
            }        
             
            return View(expn);
        }

    }
}