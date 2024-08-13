using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.OData.Query.SemanticAst;
using System.Collections.Generic;
using System.Linq;
using Udemy.BankApp.Web.Data.Context;
using Udemy.BankApp.Web.Data.Entities;
using Udemy.BankApp.Web.Data.Repositories;
using Udemy.BankApp.Web.Data.UnitOfWork;
using Udemy.BankApp.Web.Mappings;
using Udemy.BankApp.Web.Models;

namespace Udemy.BankApp.Web.Controllers
{
    public class AccountController : Controller
    {
        ////dependency injection ile contextimizi alalım.
        //private readonly BankContext _context;
        ////application user repository kullanmak için
        ////private readonly ApplicationUserRepository applicationUserRepository;

        ////repository'i dependency injection ile kullanmak için ıapplicationuserrepository üzerinden gideceğiz.
        //private readonly IApplicationUserRepository _applicationUserRepository;
        //private readonly IAccountRepository _accountRepository;
        //private readonly IUserMapper _userMapper;
        //private readonly IAccountMapper _accountMapper;
        //public AccountController(BankContext context, IApplicationUserRepository applicationUserRepository, IUserMapper userMapper, IAccountRepository accountRepository, IAccountMapper accountMapper)
        //{
        //    _context = context;
        //    _applicationUserRepository = applicationUserRepository;
        //    _userMapper = userMapper;
        //    _accountRepository = accountRepository;
        //    _accountMapper = accountMapper;
        //}

        //şimdi generic repository'e geçelim
        //private readonly IRepository<Account> _accountRepository;
        //private readonly IRepository<ApplicationUser> _userRepository;

        //public AccountController(IRepository<Account> accountRepository, IRepository<ApplicationUser> userRepository)
        //{
        //    _accountRepository = accountRepository;
        //    _userRepository = userRepository;
        //}

        //artık repository unit of work aracılığı ile çalışacağız.
        private readonly IUow _uow;

        public AccountController(IUow uow)
        {
            _uow = uow;
        }

        public IActionResult Create(int id) //bu id a taginde bize parametre olarak gelen id. Biz bunu şöyle sorgulayacağız applicationuser classındaki id ile bu gelen id eşitse kullanıcının bilgilerini getir diyeceğiz.
        {   //parametre ile gelen id ile application user classındaki id eşit olan kullanıcılarının bilgilerini getir
            //var userInfo = _context.ApplicationUsers.Select(x=>new UserListModel
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    SurName = x.SurName,
            //}).SingleOrDefault(x => x.Id == id);

            //artık applicationUserRepository olduğu için kodumuzu şu şekilde kullanmalıyız ki tek bir yerden yönetebilelim ve DRY ile çelişmeyellim
            //var userInfo = _userMapper.MapToUserList( _applicationUserRepository.GetById(id));
            var userInfo = _uow.GetRepository<ApplicationUser>().GetById(id);
            return View(new UserListModel
            {
                Id = userInfo.Id,
                Name = userInfo.Name,
                SurName = userInfo.SurName
            });
        }

        //Şimdi gelelim ekleme işlemine
        //ben nasıl userları listelerken userlistmodel oluşturduysam accounteklerken AccountCreateModel oluşturmam gerek.
        //ilgili modelimizi oluşturduk şimdi post metodunu yazalım.
        [HttpPost]
        public IActionResult Create(AccountCreateModel model)
        {   //biz direkt böyle kullanırsak create metodu bizden account türünde parametre bekliyor ondan dolayı AccountCreateModeli accounta dönüştürelim IAccountMapper ile.

            //_accountRepository.Create(_accountMapper.Map(model));
            //_context.SaveChanges();

            _uow.GetRepository<Account>().Create(new Account
            {
                AccountNumber = model.AccountNumber,
                Balance = model.Balance,
                ApplicationUserId = model.ApplicationUserId
            });
            _uow.SaveChanges();
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult GetByUserId(int userId) 
        {   //benim getbyuserid diye generic içinde bir metot yok o zaman ıqueryable olarak dönelim bunu.

            //burada yapmam gereken ilk şey şu; bir query oluşturmak
            var query = _uow.GetRepository<Account>().GetQueryable();
            //şimdi query şu şekilde parametre olarak giden userid ile nesnenin applicationuserid eşit olanları getir
            var accounts = query.Where(x => x.ApplicationUserId == userId).ToList();//enumerable dönecek
           //ilgili userid ye eşit olan kullanıcıların verilerini çekmek için
            var user = _uow.GetRepository<ApplicationUser>().GetById(userId);

            var list = new List<AccountListModel>();
            foreach (var account in accounts)
            {
                list.Add(new AccountListModel()
                {
                    Id= account.Id,
                    AccountNumber = account.AccountNumber,
                    ApplicationUserId= account.ApplicationUserId,
                    Balance = account.Balance,
                    
                });
            }
            ViewBag.FullName = user.Name + " " + user.SurName;
            return View(list);
        }

        [HttpGet]
        public IActionResult SendMoney(int accountId)
        {
            var query = _uow.GetRepository<Account>().GetQueryable();

            //benim bana gelen account hariç diğer accountları listeleyen bir işe ihtiyacım var
            var accounts = query.Where(x=>x.Id != accountId).ToList();

            //doğrudan entity geri dönmüyorduk
            var list = new List<AccountListModel>();
            foreach(var account in accounts)
            {
                list.Add(new AccountListModel(){
                    AccountNumber = account.AccountNumber,
                    ApplicationUserId = account.ApplicationUserId,
                    Balance = account.Balance,
                    Id = account.Id
                });
            }

            //benim accountid ye ihtiyacım olacak viewbag ile onu viewe gönderelim oradan post ile göndeririz.
            ViewBag.SenderId = accountId;
            return View(new SelectList(list,"Id","AccountNumber"));
        }

        [HttpPost]      
        public IActionResult SendMoney(SendMoneyModel model)
        {
            //benim ilk önce gönderen hesabı yani senderid çekmem gerek.
            var senderAccount = _uow.GetRepository<Account>().GetById(model.SenderId);
            senderAccount.Balance -= model.Amount;
            _uow.GetRepository<Account>().Update(senderAccount);

            //şimdi giden hesabı çekelim
            var account = _uow.GetRepository<Account>().GetById(model.AccountId);
            account.Balance += model.Amount;
            _uow.GetRepository<Account>().Update(account);

            _uow.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
