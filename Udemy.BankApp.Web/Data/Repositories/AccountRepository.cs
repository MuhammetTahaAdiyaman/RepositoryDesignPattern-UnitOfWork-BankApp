using Udemy.BankApp.Web.Data.Context;
using Udemy.BankApp.Web.Data.Entities;

namespace Udemy.BankApp.Web.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankContext _context;

        public AccountRepository(BankContext context)
        {
            _context = context;
        }

        public void Create(Account account)
        {   //repository de entityler üzerinden çalışırız biz şimdi dependency injection için IAccountRepository de oluşturmamız gerek.
            //biz burada bir account istiyoruz ama accountcontroller create post metoduna AccountCreateModel geliyor bunuda çevirmemiz gerekecek. Yani AccountCreateModeli account'a çevirelim ki create metodumuz parametresini düzgün alsın.
            _context.Accounts.Add(account);
            _context.SaveChanges();

            //şimdi _context.Accounts == _context.Set<Account>() ile aynı anlama gelmekteedir.
            //biz her entity için oturup repository class içinde metot oluşturamayız bunun için generic bir repository oluşturmamız gerek hadi oluşturalım.
        }
    }
}
