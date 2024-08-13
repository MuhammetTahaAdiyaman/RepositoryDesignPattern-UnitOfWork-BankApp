using System.Collections.Generic;

namespace Udemy.BankApp.Web.Data.Entities
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        //ilişki için
        public List<Account> Accounts { get; set; } //Bir applicationuser'ın birden fazla account'ı olabilir 1:n
    }
}
