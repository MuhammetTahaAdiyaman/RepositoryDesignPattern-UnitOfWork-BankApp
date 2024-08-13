using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Udemy.BankApp.Web.Data.Entities;

namespace Udemy.BankApp.Web.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(x=>x.Name).HasMaxLength(200);
            builder.Property(x=>x.Name).IsRequired();
            builder.Property(x => x.SurName).HasMaxLength(250);
            builder.Property(x => x.SurName).IsRequired();

            //ilişkimzi kuralım 1:n (Not:2 configuration dosyasında da kurulabilir ama burada kuralım)
            builder.HasMany(x => x.Accounts).WithOne(x => x.ApplicationUser).HasForeignKey(x => x.ApplicationUserId);

        }
    }
}
