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
using SecureSoftwareApplication.Services;

namespace SecureSoftwareApplication.Controllers
{
    public class FilesController : RootController
    {
        

        // GET: Files
        public ActionResult Index(int id)
        {
            var transactions = db.Transactions.Where(f => f.Job.JobID == id);
            List<File> files = new List<Models.File>();

            ViewBag.Job = id;

            foreach(var transaction in transactions)
            {
                files.Add(transaction.File);
            }
            return View(files);
        }

        // GET: Files/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            File file = db.Files.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // GET: Files/Create
        public ActionResult Create(int id)
        {
            var job = db.Jobs.Find(id);

            if (job != null || job.closed==true)
            {
                FileViewModel file = new FileViewModel
                {
                    job = job.JobID

                };
                return View(file);
            }

            else
            {
                return RedirectToAction("Home", "Index");
            }



            
        }

        // POST: Files/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FileViewModel files)
        {
            if (ModelState.IsValid)
            {
                File addFile = new Models.File()
                {
                    Contents = files.Contents,
                    Name = files.Name,
                    Folder = files.Folder,
                    PubDate = DateTime.Now.Date,
                    TimeStamp = DateTime.Now.Date, 
                    Size = 0
                };

                // Files is looking for the corresponding ID in the view
                HttpPostedFileBase content = Request.Files["file"];

                //calculates the size of the file to Mb 
                int size = (content.ContentLength / 1024) / 1024;

                addFile.Size = size;

                if (content != null)
                {
                    FileStorageService fss = new FileStorageService();

                    addFile.Source = fss.Upload(content);
                }




                db.Files.Add(addFile);


                var job = db.Jobs.Find(files.job);

                JobTransaction jt = new JobTransaction()
                {

                    File = addFile,
                    Job = job ,
                    TimeStamp = DateTime.Now.Date, 
                };


                db.SaveChanges();

                db.Transactions.AddOrUpdate(jt);

                db.SaveChanges();

                return RedirectToAction("Index", "JobTransactions", new { id = files.job });
            }

            return View(files);
        }

        // GET: Files/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            File file = db.Files.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // POST: Files/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FileID,Contents,PubDate,Source,Folder,Name,TimeStamp")] File file)
        {
            if (ModelState.IsValid)
            {
                db.Entry(file).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(file);
        }

        // GET: Files/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            File file = db.Files.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // POST: Files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            File file = db.Files.Find(id);
            db.Files.Remove(file);
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
