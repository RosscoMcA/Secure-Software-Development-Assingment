using Microsoft.AspNet.Identity;
using SecureSoftwareApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecureSoftwareApplication.Controllers
{
    /// <summary>
    /// Controller handles the general operations of the website
    /// </summary>
    public class RootController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Checks to see if the current user is an admin
        /// </summary>
        /// <returns>True if an admin is found, false otherwise</returns>
        public bool isAdmin()
        {
            var account = getAccount(); 
            if(account != null)
            {
                switch (account.AccountType)
                {
                    case AccountType.Admin: 
                        return true;
                    case AccountType.Employee:
                        return false;
                    default: return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks to see if the current user is an employee
        /// </summary>
        /// <returns>True if an employee is found, false otherwise</returns>
        public bool isEmployee()
        {
            var account = getAccount();
            if (account != null)
            {
                switch (account.AccountType)
                {
                    case AccountType.Employee:
                        return true;
                    case AccountType.Admin:
                        return false;
                    default: return false;
                }
            }

            return false;
        }


        /// <summary>
        /// gets the current users account
        /// </summary>
        /// <returns>The account if it is found, otherwise null</returns>
        public Account getAccount()
        {
            var userID = User.Identity.GetUserId();

            var accountFound = db.Accounts.Where(u => u.Id == userID).SingleOrDefault();
            if (accountFound != null) return accountFound;
            else
            {
                return null;
            }
        }
    }
}