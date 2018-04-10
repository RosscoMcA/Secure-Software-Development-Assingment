using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SecureSoftwareApplication.Models;

namespace SecureSoftwareApplication.Controllers
{
    public class JobTransactionsController : RootController
    {

        public ActionResult Finalise(int id)
        {
            var job = db.Jobs.Find(id);

            foreach (var item in job.Files.ToList())
            {
                if (item.File.Size > 500) job.authorised = false;
            }


            return RedirectToAction("Index", "Jobs");
        }

        // GET: JobTransactions
        public ActionResult Index(int id)
        {
            var job = db.Jobs.Find(id);

            if (job != null)
            {
                ViewBag.Job = job.JobID;
                return View(db.Transactions.Where(jt => jt.Job.JobID == id).ToList());

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: JobTransactions/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobTransaction jobTransaction = db.Transactions.Find(id);
            if (jobTransaction == null)
            {
                return HttpNotFound();
            }
            return View(jobTransaction);
        }

        // GET: JobTransactions/Create
        public ActionResult Create()
        {
            return View();
        }

       

        // GET: JobTransactions/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobTransaction jobTransaction = db.Transactions.Find(id);
            if (jobTransaction == null)
            {
                return HttpNotFound();
            }
            return View(jobTransaction);
        }

        // POST: JobTransactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,TimeStamp")] JobTransaction jobTransaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobTransaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jobTransaction);
        }

        // GET: JobTransactions/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobTransaction jobTransaction = db.Transactions.Find(id);
            if (jobTransaction == null)
            {
                return HttpNotFound();
            }
            return View(jobTransaction);
        }

        // POST: JobTransactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            JobTransaction jobTransaction = db.Transactions.Find(id);
            db.Transactions.Remove(jobTransaction);
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
    }
}
