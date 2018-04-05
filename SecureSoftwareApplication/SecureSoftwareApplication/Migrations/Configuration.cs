namespace SecureSoftwareApplication.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SecureSoftwareApplication.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

        }

        protected override void Seed(SecureSoftwareApplication.Models.ApplicationDbContext context)
        {
            if (System.Diagnostics.Debugger.IsAttached ==false)
                System.Diagnostics.Debugger.Launch();

            try
            {

                if (!context.Users.Any())
                {

                    Account admin = new Account
                    {
                        Email = "Admin@admin.com", 
                        UserName ="ActuallyAdmin123", 
                        PhoneNumber="07714922341", 
                        AccountType = AccountType.Admin
                       
                    };

                    Account employee = new Account
                    {
                        Email = "Employee123@fxtrade.com",
                        UserName = "Employee101",
                        PhoneNumber = "08459517880",
                        AccountType = AccountType.Employee
                    };

                    CreateAccount(admin, "Admin1!", context);
                    CreateAccount(employee, "Employee456", context);

                    File file1 = new File
                    {
                        Contents = "Empty seed data",
                        Folder = "Fake folder",
                        PubDate = DateTime.Now.Date,
                        Name = "Empty seed File",
                        Source = "FakeSRC",
                        TimeStamp = DateTime.Now.AddDays(-1)
                    };

                    File file2 = new File
                    {
                        Contents = "Empty seed data number 2 ",
                        Folder = "Fake folder 2",
                        PubDate = DateTime.Now.Date,
                        Name = "Empty seed File 2",
                        Source = "FakeSRC2",
                        TimeStamp = DateTime.Now.AddDays(-2)
                    };

                    context.Files.AddOrUpdate(file1);
                    context.Files.AddOrUpdate(file2);

                    Job job = new Job
                    {
                        Author = employee,
                        authorised = true,
                        Destination = Destination.HongKong,
                        Start = DateTime.Now.Date,
                        End = DateTime.Now.AddDays(2),
                        isPublic = true,
                        state = State.Active,
                        Type = JobType.Continous,

                    };

                    JobTransaction jt1 = new JobTransaction
                    {
                        File = file1,
                        Job = job,
                        TimeStamp = DateTime.Now.Date
                    };

                    JobTransaction jt2 = new JobTransaction
                    {
                        File = file2,
                        Job = job,
                        TimeStamp = DateTime.Now.Date
                    };

                    

                    context.Jobs.AddOrUpdate(job);

                    context.SaveChanges();

                }
            }
            catch(Exception e)
            {
                
            }
        }

        private void CreateAccount(Account account, string password, ApplicationDbContext context)
        {
            //Creates and intiailises the componients of adding the admin
            var userStore = new UserStore<Account>(context);
            var userManager = new UserManager<Account>(userStore);
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 1,
                RequireNonLetterOrDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
                RequireDigit = false
            };
            //Enum asignment for the account type
            
            //Adds the admin to the database 
            var userCreateResult = userManager.Create(account, password);

            //If the creation of the Staff has failed throw exception
            if (!userCreateResult.Succeeded)
            {
                throw new Exception(string.Join(";", userCreateResult.Errors));
            }
        }

    }
}

