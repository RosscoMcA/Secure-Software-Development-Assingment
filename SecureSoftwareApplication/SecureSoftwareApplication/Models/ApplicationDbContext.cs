using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SecureSoftwareApplication.Models
{
    /// <summary>
    /// Database context class creates all tables
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        //Table listings
        public IDbSet<Account>Accounts { get; set; }
        public IDbSet<File>Files { get; set; }
        public IDbSet<Job>Jobs { get; set; }
        public IDbSet<JobTransaction>Transactions { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}