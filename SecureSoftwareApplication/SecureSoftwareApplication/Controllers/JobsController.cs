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

namespace SecureSoftwareApplication.Controllers
{
    public class JobsController : RootController
    {
       

        // GET: Jobs
        public ActionResult Index()
        {
            if (getAccount() != null&& isAdmin()==false)
            {
                var jobs = db.Jobs.Where(q => q.Author.Id == getAccount().Id || q.isPublic);
                return View(jobs);

            }
            if (isAdmin())
            {
                var jobs = db.Jobs.Where(q => q.authorised==false);
                
                return View(jobs);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Jobs/Details/5
        public ActionResult Details(int? id)
        {
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
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobID,Start,End,Type,state,authorised,isPublic,Destination")] Job job)
        {
            if (ModelState.IsValid)
            {
                db.Jobs.Add(job);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(job);
        }

        // GET: Jobs/Edit/5
        public ActionResult Edit(int? id)
        {
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

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobID,Start,End,Type,state,authorised,isPublic,Destination")] Job job)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(job);
        }

        // GET: Jobs/Delete/5
        public ActionResult Delete(int? id)
        {
            

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

        public ActionResult Authorise(int id)
        {
            if (isAdmin() == false|| getAccount()==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "You are not allowed here");
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


        public ActionResult Deny (int id)
        {
            if (isAdmin() == false || getAccount() == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "You are not allowed here");
            }
            else
            {
                var job = db.Jobs.Find(id);

                if (job != null)
                {


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
