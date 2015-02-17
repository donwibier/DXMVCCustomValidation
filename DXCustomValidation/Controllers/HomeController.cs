using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DXCustomValidation.Models;
using DevExpress.Web.Mvc;

namespace DXCustomValidation.Controllers
{
    public class HomeController : Controller
    {
        private DXCustomValidationContext db = new DXCustomValidationContext();

        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View(db.People.ToList());
        }

        //
        // GET: /Home/Details/5

        public ActionResult Details(int id = 0)
        {
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        //
        // GET: /Home/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Home/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Person person)
        {
            if (ModelState.IsValid)
            {
                db.People.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(person);
        }

        //
        // GET: /Home/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        //
        // POST: /Home/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        //
        // GET: /Home/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        //
        // POST: /Home/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.People.Find(id);
            db.People.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

		  DXCustomValidation.Models.DXCustomValidationContext db1 = new DXCustomValidation.Models.DXCustomValidationContext();

		  [ValidateInput(false)]
		  public ActionResult GridViewPartial()
		  {
				var model = db1.People;
				return PartialView("_GridViewPartial", model.ToList());
		  }

		  [HttpPost, ValidateInput(false)]
		  public ActionResult GridViewPartialAddNew(DXCustomValidation.Models.Person item)
		  {
				var model = db1.People;
				if (ModelState.IsValid)
				{
					 try
					 {
						  model.Add(item);
						  db1.SaveChanges();
					 }
					 catch (Exception e)
					 {
						  ViewData["EditError"] = e.Message;
					 }
				}
				else
					 ViewData["EditError"] = "Please, correct all errors.";
				return PartialView("_GridViewPartial", model.ToList());
		  }
		  [HttpPost, ValidateInput(false)]
		  public ActionResult GridViewPartialUpdate(DXCustomValidation.Models.Person item)
		  {
				var model = db1.People;
				if (ModelState.IsValid)
				{
					 try
					 {
						  var modelItem = model.FirstOrDefault(it => it.Id == item.Id);
						  if (modelItem != null)
						  {
								this.UpdateModel(modelItem);
								db1.SaveChanges();
						  }
					 }
					 catch (Exception e)
					 {
						  ViewData["EditError"] = e.Message;
					 }
				}
				else
					 ViewData["EditError"] = "Please, correct all errors.";
				return PartialView("_GridViewPartial", model.ToList());
		  }
		  [HttpPost, ValidateInput(false)]
		  public ActionResult GridViewPartialDelete(System.Int32 Id)
		  {
				var model = db1.People;
				if (Id >= 0)
				{
					 try
					 {
						  var item = model.FirstOrDefault(it => it.Id == Id);
						  if (item != null)
								model.Remove(item);
						  db1.SaveChanges();
					 }
					 catch (Exception e)
					 {
						  ViewData["EditError"] = e.Message;
					 }
				}
				return PartialView("_GridViewPartial", model.ToList());
		  }
    }
}