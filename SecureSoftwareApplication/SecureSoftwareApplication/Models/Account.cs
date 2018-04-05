namespace SecureSoftwareApplication.Models
{
    /// <summary>
    /// Model Represents the details of the Account table
    /// </summary>
    public class Account :ApplicationUser
    {
        public string Name { get; set; }

        public AccountType AccountType { get; set; }


    }

    public enum AccountType { Admin, Employee}
}