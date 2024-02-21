using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestCompanyEntity.Models;

namespace TestCompanyEntity.Controllers
{
    public class crudsController : Controller
    {
        private Task_CompanyEntities db = new Task_CompanyEntities();

        // GET: cruds
        public ActionResult Index()
        {
            var cruds = db.cruds.Include(c => c.Country1).Include(c => c.State1);
            return View(cruds.ToList());
        }

        // GET: cruds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crud crud = db.cruds.Find(id);
            if (crud == null)
            {
                return HttpNotFound();
            }
            return View(crud);
        }

        // GET: cruds/Create
        public ActionResult Create()
        {
            ViewBag.Country = new SelectList(db.Countries, "idCountry", "CountryName");
            ViewBag.State = new SelectList(db.States, "idState", "StateName");
            return View();
        }

        // POST: cruds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name,Gender,Email,PhoneNumber,Country,State,Age")] crud crud)
        {
            if (ModelState.IsValid)
            {
                db.cruds.Add(crud);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Country = new SelectList(db.Countries, "idCountry", "CountryName", crud.Country);
            ViewBag.State = new SelectList(db.States, "idState", "StateName", crud.State);
            return View(crud);
        }

        // GET: cruds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crud crud = db.cruds.Find(id);
            if (crud == null)
            {
                return HttpNotFound();
            }
            ViewBag.Country = new SelectList(db.Countries, "idCountry", "CountryName", crud.Country);
            ViewBag.State = new SelectList(db.States, "idState", "StateName", crud.State);
            return View(crud);
        }

        // POST: cruds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,Gender,Email,PhoneNumber,Country,State,Age")] crud crud)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crud).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Country = new SelectList(db.Countries, "idCountry", "CountryName", crud.Country);
            ViewBag.State = new SelectList(db.States, "idState", "StateName", crud.State);
            return View(crud);
        }

        // GET: cruds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crud crud = db.cruds.Find(id);
            if (crud == null)
            {
                return HttpNotFound();
            }
            return View(crud);
        }

        // POST: cruds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            crud crud = db.cruds.Find(id);
            db.cruds.Remove(crud);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //public JsonResult StatesBind(int ? )
        //{
        //    var States= db.States.Where(c=>c.idCountry== CountryID).ToList();
        //    return Json(States,JsonRequestBehavior.AllowGet);
        //}
        public JsonResult StatesBind(int? CountryID)
        {
            try
            {
                var states = db.States.Where(c => c.idCountry == CountryID).Select(s => new { id = s.idState, name = s.StateName }).ToList();
                return Json(states, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // Logger.LogError(ex);
                return Json(new { error = "An error occurred while fetching states." }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
