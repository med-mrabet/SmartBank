

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartBank.Domain.Entities;
using SmartBank.Identity.Models;

namespace SmartBank.Identity.DbContext
{
    public class IdentitySmartBankContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
        public IdentitySmartBankContext(DbContextOptions<IdentitySmartBankContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply all configurations from the current assembly
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentitySmartBankContext).Assembly);
            modelBuilder.Entity<ApplicationUser>(b =>
            {
                // Each User can have many UserClaims
                b.Property<Guid>("Id");
                
                b.HasMany<Account>()
                 .WithOne()
                 .HasForeignKey(uc => uc.UserId)
                 .IsRequired();

                b.HasMany<Notification>()
                .WithOne()
                .HasForeignKey(uc => uc.UserId)
                .IsRequired();

                b.HasMany<Transaction>()
               .WithOne()
               .HasForeignKey(uc => uc.FromAccountId)
               .IsRequired();

                b.HasMany<Transaction>()
              .WithOne()
              .HasForeignKey(uc => uc.ToAccountId)
              .IsRequired();
            });
            modelBuilder.Entity<IdentityRole>().HasData([new IdentityRole {
                Id = "814a94b3-dde2-4d27-bcda-f681ba53c4ff",
                Name="Admin",
                NormalizedName="ADMIN"

            },
            new IdentityRole{
                Id="f3295817-44d3-4df5-84e5-3987dbc976c6",
                 Name="User",
                NormalizedName="USER"
            }
            ]);
        }
        // Define DbSet properties for your entities

 
   
    }
}
