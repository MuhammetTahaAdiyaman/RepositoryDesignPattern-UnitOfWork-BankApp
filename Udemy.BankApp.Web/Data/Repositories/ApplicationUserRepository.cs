using System.Collections.Generic;
using System.Linq;
using Udemy.BankApp.Web.Data.Context;
using Udemy.BankApp.Web.Data.Entities;

namespace Udemy.BankApp.Web.Data.Repositories
{//biz applicationuser ile ilgili işlemleri bu repository'de yapacağız.
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly BankContext _context;
        public ApplicationUserRepository(BankContext context)
        {
            _context = context;
        }
        //home controller içinde list olarak getiriyoruz idsiz.
        //Not:burada önemli bir nokta var. Repository'de entity dönüyoruz bunun şimdilik bu şekilde olması gerekitğini kabul edelim.
        //normalde biz direkt entity değil de model dönmemiz gerekiyordu ya son kullanıcı için ondan dolayı şimdilik bu şekilde kabul edelim ileride n tier architecture da bunun neden bu şekilde olduğunu anlayacağız.
        public List<ApplicationUser> GetAll()
        {
            return _context.ApplicationUsers.ToList();
        }

        //action controller içinde id'li şekilde getirmek için de
        public ApplicationUser GetById(int id)
        {
            return _context.ApplicationUsers.SingleOrDefault(x=>x.Id == id);
        }
    }
}
