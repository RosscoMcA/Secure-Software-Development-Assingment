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
    /// <summary>
    /// Controller handles the user control operations for an administrator. 
    /// </summary>
    public class AdminOptionsController : RootController
    {
        

        // GET: AdminOptions
        public ActionResult Index()
        {
            if (!isAdmin())
            {
                return RedirectToAction("Index", "Home");
            }
            return View(db.ApplicationUsers.ToList());
        }

        // GET: AdminOptions/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.ApplicationUsers.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: AdminOptions/Create
        public ActionResult Create()
        {
            return View();
        }

       

        // POST: AdminOptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,AccountType")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.ApplicationUsers.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(account);
        }

        // GET: AdminOptions/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.ApplicationUsers.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: AdminOptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,AccountType")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: AdminOptions/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.ApplicationUsers.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: AdminOptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Account account = db.ApplicationUsers.Find(id);
            db.ApplicationUsers.Remove(account);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// handles the promotion of users from Employees to Admins
        /// </summary>
        /// <param name="id">the user to promote's ID</param>
        /// <returns>return to list page</returns>
        public ActionResult PromoteUser(string id)
        {
            var account = db.ApplicationUsers.Find(id);
            if (account != null)
            {
                account.AccountType = AccountType.Admin;
                db.ApplicationUsers.AddOrUpdate(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// handles the promotion of users from Employees to Admins
        /// </summary>
        /// <param name="id">the user to demote's ID</param>
        /// <returns>return to list page</returns>
        public ActionResult DemoteUser(string id)
        {
            var account = db.ApplicationUsers.Find(id);
            if (account != null)
            {
                account.AccountType = AccountType.Employee;
                db.ApplicationUsers.AddOrUpdate(account);
                db.SaveChanges();
                return RedirectToAction("Index");
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
