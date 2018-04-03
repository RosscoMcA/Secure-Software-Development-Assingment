namespace SecureSoftwareApplication.Models
{
    /// <summary>
    /// Model Represents the details of the Account table
    /// </summary>
    public class Account : ApplicationUser
    {

        public AccountType AccountType { get; set; }


    }

    public enum AccountType { Admin, Employee}
}