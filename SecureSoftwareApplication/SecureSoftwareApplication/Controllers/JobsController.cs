using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SecureSoftwareApplication.Models;
using System.Data.Entity.Migrations;
using SecureSoftwareApplication.Extensions;

namespace SecureSoftwareApplication.Controllers
{
    public class JobsController : RootController
    {

        /// <summary>
        /// Displays a list of jobs to a user
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.isAdmin = isAdmin();

            var account = getAccount();

            if (account != null)
            {
                if (isAdmin() == false)
                {
                    var jobs = db.Jobs.Where(q => q.Author.Id == account.Id || q.isPublic).ToList();
                    return View(jobs);

                }
                if (isAdmin())
                {
                    var jobs = db.Jobs.Where(q => q.authorised == false);

                    return View(jobs);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            else
            {
                this.AddNotification("Sorry! You are not allowed access", NotificationType.ERROR);
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Displays the contents of the job selected 
        /// </summary>
        /// <param name="id">The job wish to be viewed</param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            if (getAccount() == null)
            {
                this.AddNotification("Sorry! You are not allowed access", NotificationType.ERROR);
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // GET: Jobs/Create
        public ActionResult Create()
        {
            if (getAccount() == null)
            {
                this.AddNotification("Sorry! You are not allowed access", NotificationType.ERROR);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        /// <summary>
        /// Creates a new job to add files to
        /// </summary>
        /// <param name="job">The parameters of the job item entered by a user</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobID,Start,End,Type,state,authorised,isPublic,Destination")] Job job)
        {

            if (getAccount() == null)
            {
                this.AddNotification("Sorry! You are not allowed access", NotificationType.ERROR);
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                job.Author = getAccount();
                db.Jobs.Add(job);
                db.SaveChanges();
                return RedirectToAction("Index", "Files", new { id = job.JobID });
            }

            return View(job);
        }


        /// <summary>
        /// The job to delete from the database records
        /// </summary>
        /// <param name="id">the Job to delete</param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            if (getAccount() == null)
            {
                this.AddNotification("Sorry! You are not allowed access", NotificationType.ERROR);
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Job job = db.Jobs.Find(id);
            db.Jobs.Remove(job);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Authorises the publishing of the job before it is run by the main process
        /// </summary>
        /// <param name="id">The job to allow processing to</param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Authorise(int id)
        {
            if (isAdmin() == false|| getAccount()==null)
            {
                
              
                    this.AddNotification("Sorry! You are not allowed access", NotificationType.ERROR);
                    return RedirectToAction("Index", "Home");
              
            }
            else
            {
                var job = db.Jobs.Find(id);

                if (job != null)
                {
                    job.authorised = true;

                    db.Jobs.AddOrUpdate(job);

                    db.SaveChanges();

                    
                }
            }

            return RedirectToAction("Index");

        }

        /// <summary>
        /// Denys the publishing of a job before it is run by the main process 
        /// </summary>
        /// <param name="id">The job to deny processing to</param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Deny (int id)
        {
            if (isAdmin() == false || getAccount() == null)
            {
                this.AddNotification("Sorry! You are not allowed access", NotificationType.ERROR);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var job = db.Jobs.Find(id);

                if (job != null)
                {

                    var jt = db.Transactions.Where(r => r.Job.JobID == job.JobID).ToList();

                    foreach(var transaction in jt)
                    {
                        db.Transactions.Remove(transaction);
                        db.SaveChanges();
                    }
                    db.Jobs.Remove(job);

                    db.SaveChanges();


                }
            }

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
