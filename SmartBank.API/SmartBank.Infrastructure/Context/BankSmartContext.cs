using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartBank.Application.Persistence;
using SmartBank.Domain.Entities;
using SmartBank.Domain.Entities.BaseEntity;
using SmartBank.Shared.Models;


namespace SmartBank.Infrastructure.Context
{
    public class BankSmartContext : IdentityDbContext<ApplicationUser ,ApplicationRole ,Guid>
    {
        public BankSmartContext(DbContextOptions<BankSmartContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BankSmartContext).Assembly);
            base.OnModelCreating(modelBuilder);
            // Apply all configurations from the current assembly
            modelBuilder.Entity<Account>().HasKey(a => a.Id);
            modelBuilder.Entity<Transaction>().HasKey(a => a.Id);
 //           modelBuilder.Entity<Account>()
 //       .HasOne(a => a.User) // The navigation property in Account
 //       .WithMany() // Assuming the ApplicationUser doesn't have a navigation property back to Account
 //       .HasForeignKey(a => a.UserId);

 //           modelBuilder.Entity<Notification>()
 //       .HasOne(a => a.User) // The navigation property in Account
 //       .WithMany() // Assuming the ApplicationUser doesn't have a navigation property back to Account
 //       .HasForeignKey(a => a.UserId);

 //           modelBuilder.Entity<Transaction>()
 //       .HasOne(a => a.FromAccount) // The navigation property in Account
 //       .WithMany() // Assuming the ApplicationUser doesn't have a navigation property back to Account
 //       .HasForeignKey(a => a.FromAccountId);

 //           modelBuilder.Entity<Transaction>()
 //.HasOne(a => a.ToAccount) // The navigation property in Account
 //.WithMany() // Assuming the ApplicationUser doesn't have a navigation property back to Account
 //.HasForeignKey(a => a.ToAccountId);


            modelBuilder.Entity<ApplicationRole>(b =>
            {
                // Each Role can have many entries in the UserRole join table
              

                b.HasData([new ApplicationRole {
                    Id=Guid.Parse("3537c890-a749-4b75-8385-0628cf54d029"),
                    Name ="Admin",
                    NormalizedName="ADMIN"
                },
                new ApplicationRole {
                    Id = Guid.Parse("08710f3f-bd84-4e0b-9ce1-bf074f514745"),
                    Name ="User",
                    NormalizedName="USER"
                }
                ]);
            });
            modelBuilder.Entity<ApplicationUser>(b =>
            {
                // Each User can have many UserClaims
                b.Property<Guid>("Id");

            //    b.HasMany<Account>()
            //     .WithOne()
            //     .HasForeignKey(uc => uc.UserId)
            //     .IsRequired().OnDelete(DeleteBehavior.Restrict); ;

            //    b.HasMany<Notification>()
            //    .WithOne()
            //    .HasForeignKey(uc => uc.UserId)
            //    .IsRequired().OnDelete(DeleteBehavior.Restrict); ;

            //    b.HasMany<Transaction>()
            //   .WithOne()
            //   .HasForeignKey(uc => uc.FromAccountId)
            //   .IsRequired().OnDelete(DeleteBehavior.Restrict); ;

            //    b.HasMany<Transaction>()
            //  .WithOne()
            //  .HasForeignKey(uc => uc.ToAccountId)
            //  .IsRequired().OnDelete(DeleteBehavior.Restrict);
            });


        }
        // Define DbSet properties for your entities

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = base.ChangeTracker.Entries<BaseEntity>();
            foreach (var entityEntry in entries.Where(p=>p.State == EntityState.Added || p.State == EntityState.Modified ))
            {
               entityEntry.Entity.UpdatedAt = DateTime.UtcNow;
                if( entityEntry.State == EntityState.Added)
                {
                    entityEntry.Entity.CreatedAt = DateTime.UtcNow;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
   
    }
}
