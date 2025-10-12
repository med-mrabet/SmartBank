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
