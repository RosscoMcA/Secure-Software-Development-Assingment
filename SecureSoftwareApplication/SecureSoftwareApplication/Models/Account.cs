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

    /// <summary>
    /// Enum represents User roles
    /// </summary>
    public enum AccountType { Admin, Employee}
}