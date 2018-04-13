using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SecureSoftwareApplication.Models;
using SecureSoftwareApplication.Extensions;

namespace SecureSoftwareApplication.Controllers
{
    public class JobTransactionsController : RootController
    {
        /// <summary>
        /// Finalises the jobs and counnts the file size of the total file set
        /// </summary>
        /// <param name="id">the id of the job to finalise </param>
        /// <returns></returns>
        public ActionResult Finalise(int id)
        {

            if (getAccount() == null)
            {
                this.AddNotification("Sorry! You are not allowed to access this page", NotificationType.ERROR);
                return RedirectToAction("Home", "Index");
            }

            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var job = db.Jobs.Find(id);

            if (!job.closed)
            {

                foreach (var item in job.Files.ToList())
                {
                    if (item.File.Size > 500) job.authorised = false;
                }

            }

            return RedirectToAction("Index", "Jobs");
        }

        // GET: JobTransactions
        public ActionResult Index(int id)
        {

            if (getAccount() == null)
            {
                this.AddNotification("Sorry! You are not allowed to access this page", NotificationType.ERROR);
                return RedirectToAction("Home", "Index");
            }

            var job = db.Jobs.Find(id);

            if (job != null || !job.closed)
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
            if (getAccount() == null)
            {
                this.AddNotification("Sorry! You are not allowed to access this page", NotificationType.ERROR);
                return RedirectToAction("Home", "Index");
            }

            if (id == null)
            {
                return RedirectToAction("Index", "Home");
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

       

        // GET: JobTransactions/Delete/5
        public ActionResult Delete(string id)
        {
            if (getAccount() == null)
            {
                this.AddNotification("Sorry! You are not allowed to access this page", NotificationType.ERROR);
                return RedirectToAction("Home", "Index");
            }
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
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
