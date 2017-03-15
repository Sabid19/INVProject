using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Repository;
using DataAccess;
using System.Net;

namespace POSWeb.Controllers.Inventory
{
    public class ManufacturerController : Controller
    {
        Repository<Manufacturer> Repmnu = new Repository<Manufacturer>();
        private string oldManufacturerName = "";
        // GET: Manufacturer
        public ActionResult Index()
        {
            var menlist = Repmnu.GetAll().OrderByDescending(x => x.ID).ToList();
            return View(menlist);
        }


        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {
                //check if duplication exists
                int count = 0; //repo.ManufacturerDuplicationCheck(manufacturer);
                //if yes throw an error message
                if (count > 0)
                {
                    ViewBag.DuplicateError = "Already Exists!!";
                    return View(manufacturer);
                }
                else
                {
                    //else add new manufacturer and return to Index
                    Repmnu.Add(manufacturer);
                   // db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(manufacturer);
        }

        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer manufacturer = Repmnu.GetById(id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            oldManufacturerName = manufacturer.ManufacturerName;
            return View(manufacturer);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ManufacturerName,Description")] Manufacturer manufacturer)
        {
            if (ModelState.IsValid)
            {


                //get old object
                var original = Repmnu.GetById(manufacturer.ID);
         
                if (original.ManufacturerName != manufacturer.ManufacturerName)
                {
                    //check if duplicate exists
                    int count = 0; // repo.ManufacturerDuplicationCheck(manufacturer);

                    if (count > 0)
                    {
                        ViewBag.DuplicateError = "Already Exists!!";
                        return View(manufacturer);
                    }
                }

                Repmnu.Update(original, manufacturer);

                return RedirectToAction("Index");
            }
            return View(manufacturer);
        }



    }
}