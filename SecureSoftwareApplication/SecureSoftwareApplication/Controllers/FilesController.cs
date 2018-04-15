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
using SecureSoftwareApplication.Extensions;

namespace SecureSoftwareApplication.Controllers
{
    public class FilesController : RootController
    {
        

        /// <summary>
        /// Displays a list of files uploaded to one job
        /// </summary>
        /// <param name="id">the job assosiated with these files</param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            if (getAccount() == null)
            {
                this.AddNotification("Sorry! You are not allowed to access this page", NotificationType.ERROR);
                return RedirectToAction("Home", "Index");
            }
            if (id == null)
            {
                this.AddNotification("Sorry! Something went wrong", NotificationType.ERROR);
                return RedirectToAction("Index", "Home");
            }

            var transactions = db.Transactions.Where(f => f.Job.JobID == id);
            List<File> files = new List<Models.File>();

            ViewBag.Job = id;

            foreach(var transaction in transactions)
            {
                files.Add(transaction.File);
            }
            return View(files);
        }

        /// <summary>
        /// Provides the details of the file
        /// </summary>
        /// <param name="id">The file selected</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            if (getAccount() == null)
            {
                this.AddNotification("Sorry! You are not allowed to access this page", NotificationType.ERROR);
                return RedirectToAction("Home", "Index");
            }
            if (id == null)
            {
                this.AddNotification("Sorry! Something went wrong", NotificationType.ERROR);
                return RedirectToAction("Index", "Home");
            }
            File file = db.Files.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        /// <summary>
        /// Sets the values of the new files before it is sent to the user to enter their values
        /// </summary>
        /// <param name="id">The job assosiated with this file </param>
        /// <returns></returns>
        public ActionResult Create(int id)
        {
            if (getAccount() == null)
            {
                this.AddNotification("Sorry! You are not allowed to access this page", NotificationType.ERROR);
                return RedirectToAction("Home", "Index");
            }

            if (id == null)
            {
                this.AddNotification("Sorry! Something went wrong", NotificationType.ERROR);
                return RedirectToAction("Index", "Home");
            }

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

        /// <summary>
        /// The creation of a new file entry 
        /// </summary>
        /// <param name="files">the values entered by the user for this file</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FileViewModel files)
        {
            if (getAccount() == null)
            {
                this.AddNotification("Sorry! You are not allowed to access this page", NotificationType.ERROR);
                return RedirectToAction("Home", "Index");
            }

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
                    //Where a file exists in this entry, upload to cloud.
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

        

        

        /// <summary>
        /// Deletes a specific file from the database
        /// </summary>
        /// <param name="id">the file to remove</param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            if (getAccount() == null)
            {
                this.AddNotification("Sorry! You are not allowed to access this page", NotificationType.ERROR);
                return RedirectToAction("Home", "Index");
            }

            if (id == null)
            {

                this.AddNotification("Sorry! Something went wrong", NotificationType.ERROR);
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
