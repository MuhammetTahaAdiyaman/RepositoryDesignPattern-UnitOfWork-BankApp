using Microsoft.EntityFrameworkCore;
using Udemy.BankApp.Web.Data.Configurations;
using Udemy.BankApp.Web.Data.Entities;

namespace Udemy.BankApp.Web.Data.Context
{
    public class BankContext:DbContext
    {   
        //ilgili tablolarımızı ekleyelim
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        
        
        
        //biz önceden nasıl yapıyorduk OnConfiguring metodu içine connection stringimizi yazabiliyorduk.
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("server=TAHA\\SQLEXPRESS; database=BankDb; integrated security=true;");
        //    base.OnConfiguring(optionsBuilder);
        //}
        //ancak artık biz dependency injection ile constructor ile bunu sağlıyacağız
        //dependency injection öğrendiğimize göre artık biz dbcontextimizi efcore aracılığı ile DI geçebiliyoruz. HomeControllera git Index action'a bak oradan devam ediyorum.
        //constructor ile ikinci overloadında bir options gönderebiliyoruz. Bu options base options'a gidip oradaki OnConfiguring metodunu ayağa kaldırıyor.
        public BankContext(DbContextOptions<BankContext> options) : base(options) 
        {
            //bankcontext options'ı startup tarafından buraya göndereceğiz. buradaki options base options'a gidecek ve onconfiguring metodunu ayağa kaldıracak
        }


        //ilgili configuration dosyalarını ekleyelim. Ve migration dosyalarımızı oluşturalım.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration(new  AccountConfiguration());
        }
    }
}
