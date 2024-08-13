using System.Collections.Generic;
using System.Linq;
using Udemy.BankApp.Web.Data.Entities;
using Udemy.BankApp.Web.Models;

namespace Udemy.BankApp.Web.Mappings
{
    public class UserMapper : IUserMapper
    {
        //dependency injection ile kullanmak için IUserMapper interface oluşturalım.

        //biz applicationuserepository'de direkt entity dönüyorduk ancak biz entity dönmenin yanlış olduğundan model dönmemiz gerektiğinden
        //bahsetmiştik bundan dolayı biz burada bir çevirme işlemi gerçekleştireceğiz.

        //biz repositoryde iki metot kullandık biri liste olarak diğeri de tekil olarak getiriyordu burada da ona göre bir metot işlemi yapacağız.
        public List<UserListModel> MapToListOfUserList(List<ApplicationUser> users)
        {
            return users.Select(x => new UserListModel
            {
                Id = x.Id,
                Name = x.Name,
                SurName = x.SurName
            }).ToList();
        }

        public UserListModel MapToUserList(ApplicationUser user) 
        {
            return new UserListModel
            {
                Id = user.Id,
                Name = user.Name,
                SurName = user.SurName,
            };
        }
    }
}
