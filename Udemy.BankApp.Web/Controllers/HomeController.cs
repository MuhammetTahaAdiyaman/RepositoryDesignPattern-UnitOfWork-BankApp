using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Udemy.BankApp.Web.Data.Context;
using Udemy.BankApp.Web.Data.Entities;
using Udemy.BankApp.Web.Data.Repositories;
using Udemy.BankApp.Web.Data.UnitOfWork;
using Udemy.BankApp.Web.Mappings;
using Udemy.BankApp.Web.Models;

namespace Udemy.BankApp.Web.Controllers
{
    public class HomeController : Controller
    {
        //dependency injection ile dbcontextimizi şu şekil ele alabiliyoruz.
        private readonly BankContext _context;
        //repository kullanmak için
        //private readonly ApplicationUserRepository applicationUserRepository;

        //repository'i artık dependecny injection ile kullanmak için interfacesini çağırıyoruz
        //private readonly IApplicationUserRepository _applicationUserRepository;

        //repository entity döndüğü için çevirmek için IUserMapper kullanmalıyız
        private readonly IUserMapper _userMapper;

        //unit of work kullanmak için
        private readonly IUow _uow;
        public HomeController(BankContext context, IApplicationUserRepository applicationUserRepository, IUserMapper userMapper, IUow uow)
        {
            _context = context;
            //_applicationUserRepository = applicationUserRepository;
            _userMapper = userMapper;
            _uow = uow;
        }

        public IActionResult Index()
        {
            //şu şekilde _bankcontextimize ulaşabiliyoruz. Ancak dbcontext classında bazı configure ayarlar yapmak gerekiyor.
            //_bankContext.
            //direkt entity yerine biz userlistmodel'i döndürmemiz gerek. 
            /*Not:neden entity göndermiyoruz ileride validasyonları da göreceğiz işin içine girdiği zaman çıkılmaz bir hal almakta
            mesela create metodu var diyelim birinde zorunlu tc alanı var birinde yok şimdi biz direkt entity'e required ibaresi
            koyarsak birinden birini yapamaz hale geleceğiz. Bu gibi durumlardan kurtulmak ve spesifik olarak çalışmak adına modeller
            ile çalışmalıyız. 
             */
            //UserListModel döndürebilmek için Select() kullanmalıyız.
            //return View(_context.ApplicationUsers.Select(x=>new UserListModel
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    SurName = x.SurName
                
            //}).ToList());
            //artık index.cshtml'e gidip model'e userlistmodel verebiliriz.

            //artık yukarıdaki kod yerine direkt repository kullanacağız ki DRY prensibi ile çelişmeyelim.
            //bir değişiklik yapılacaksa gidip repository üzerinden yani tek bir yerden değişiklik yapıp programı yönetebileceğiz.
            return View(_userMapper.MapToListOfUserList(_uow.GetRepository<ApplicationUser>().GetAll()));
        }
    }
}
