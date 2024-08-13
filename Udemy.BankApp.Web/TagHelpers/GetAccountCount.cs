using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;
using Udemy.BankApp.Web.Data.Context;

namespace Udemy.BankApp.Web.TagHelpers
{
    [HtmlTargetElement("getAccountCount")]
    public class GetAccountCount : TagHelper
    {
        //ilk önce context nesnesine ulaşayımki kişilerin banka hesap sayısı için veritabanına sorgu yapabileyim.
        private readonly BankContext _context;
        //peki ben neye göre banka hesabını çekeceğim. ApplicationUserIdsi bana gelenleri getireceğim.
        public int ApplicationUserId { get; set; }
        public GetAccountCount(BankContext context)
        {
            _context = context;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
           var accountCount = _context.Accounts.Count(x=> x.ApplicationUserId == ApplicationUserId); //account nesnesinde yer alan applicationuserid ile benim parametre olarak gönderdiğim applicationuserid eşit olanları say ve getir.
           var html = $"<span class='badge bg-danger'>{accountCount}</span>";
           output.Content.SetHtmlContent(html);
        }
    }
}
