using Udemy.BankApp.Web.Data.Entities;

namespace Udemy.BankApp.Web.Data.Repositories
{
    public interface IAccountRepository
    {
        void Create(Account account);
    }
}
