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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using SecureSoftwareApplication.Extensions;

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
            if (isAdmin() == false || getAccount() == null)
            {
                this.AddNotification("Sorry! You do not have permisson to access this page!", NotificationType.ERROR);
                return RedirectToAction("Index", "Home");
            }
            return View(db.Accounts.ToList());
        }

        // GET: AdminOptions/Details/5
        public ActionResult Details(string id)
        {

            if (isAdmin() == false || getAccount() == null)
            {
                this.AddNotification("Sorry! You do not have permisson to access this page!", NotificationType.ERROR);
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                this.AddNotification("Error, unexpected event", NotificationType.ERROR);
                return RedirectToAction("Index", "Home");
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: AdminOptions/Create
        public ActionResult Create()
        {
            return RedirectToAction("Register", "Account");
        }



        // POST: AdminOptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Create([Bind(Include = "Email,Name,PhoneNumber,Username,AccountType, Password, ConfirmPassword")] RegisterViewModel account)
        {
            var UserStore = new UserStore<Account>(db);
            var UserManager = new UserManager<Account>(UserStore);

            if (isAdmin() == false || getAccount() == null)
            {
                this.AddNotification("Sorry! You do not have permisson to access this page!", NotificationType.ERROR);
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {

                Account finalAccount = new Models.Account()
                {
                    Email = account.Email,
                    UserName = account.Username,
                    PhoneNumber = account.PhoneNumber,
                    AccountType = account.AccountType,
                    Name = account.Name
                };

                //Adds the admin to the database 
                var userCreateResult = UserManager.Create(finalAccount, account.Password);
                if (userCreateResult.Succeeded)
                {
                    var client = new DpapiDataProtectionProvider("ASP.NET Identity");
                    UserManager.UserTokenProvider = new DataProtectorTokenProvider<Account>(
                    client.Create("ASP.NET Identity"));
                    // Send an email with this link
                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(finalAccount.Id);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = finalAccount.Id, code = code }, protocol: Request.Url.Scheme);
                    await UserManager.SendEmailAsync(finalAccount.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index");
                }


                return RedirectToAction("Index");
            }

            return View(account);
        }

      

        // GET: AdminOptions/Delete/5
        public ActionResult Delete(string id)
        {

            if (isAdmin() == false || getAccount() == null)
            {
                this.AddNotification("Sorry! You do not have permisson to access this page!", NotificationType.ERROR);
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Account account = db.Accounts.Find(id);
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
            if (isAdmin() == false || getAccount() == null)
            {
                this.AddNotification("Sorry! You do not have permisson to access this page!", NotificationType.ERROR);
                return RedirectToAction("Index", "Home");
            }
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// handles the promotion of users from Employees to Admins
        /// </summary>
        /// <param name="id">the user to promote's ID</param>
        /// <returns>return to list page</returns>
        public ActionResult Promote(string id)
        {
            if (isAdmin() == false || getAccount() == null)
            {
                this.AddNotification("Sorry! You do not have permisson to access this page!", NotificationType.ERROR);
                return RedirectToAction("Index", "Home");
            }

            var account = db.Accounts.Find(id);
            if (account != null)
            {
                account.AccountType = AccountType.Admin;
                db.Accounts.AddOrUpdate(account);
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
        public ActionResult Demote(string id)
        {

            if (isAdmin() == false || getAccount() == null)
            {
                this.AddNotification("Sorry! You do not have permisson to access this page!", NotificationType.ERROR);
                return RedirectToAction("Index", "Home");
            }

            var account = db.Accounts.Find(id);
            if (account != null)
            {
                account.AccountType = AccountType.Employee;
                db.Accounts.AddOrUpdate(account);
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
