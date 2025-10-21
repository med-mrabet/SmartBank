using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SmartBank.Application.Persistence;
using SmartBank.Domain.Entities;
using SmartBank.Domain.Entities.BaseEntity;
using SmartBank.Shared.Models;


namespace SmartBank.Infrastructure.Context
{
    public class BankSmartContext : IdentityDbContext<ApplicationUser ,ApplicationRole ,Guid>
    {
        private readonly IConfiguration _configuration;

        public BankSmartContext(DbContextOptions<BankSmartContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<LogInHistory> LogInHistories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BankSmartContext).Assembly);
            base.OnModelCreating(modelBuilder);
            // Apply all configurations from the current assembly
            modelBuilder.Entity<Account>().HasKey(a => a.Id);
            modelBuilder.Entity<Transaction>().HasKey(a => a.Id);
            var hasher = new PasswordHasher<ApplicationUser>();
            var userAdmin = new ApplicationUser
            {
                Id = Guid.Parse("d2308bf6-87f0-463e-bbdb-e9e7fa1dd85e"),
                UserName = _configuration["admin-name"],
                FirstName = _configuration["admin-firstname"],
                LastName = _configuration["admin-lastname"],
                Email = _configuration["admin-mail"],
                ConcurrencyStamp = "d30fb918-1c37-47bb-a4b9-4019e08dceb8",
                PasswordHash = _configuration["admin-hashed-pwd"],
                PhoneNumber = _configuration["admin-phone-number"],
                DateOfBirth = DateTime.Parse("01/01/0001 00:00:00"),
                Address = "address"
            };
            modelBuilder.Entity<ApplicationUser>(b =>
            {
                b.HasData([
                     userAdmin
                    ]);

            });
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

            modelBuilder.Entity<ApplicationUserRole>(b =>
            {
                // Each User can have many UserClaims
                b.HasData([new ApplicationUserRole {
                    UserId = Guid.Parse("d2308bf6-87f0-463e-bbdb-e9e7fa1dd85e"),
                    RoleId = Guid.Parse("3537c890-a749-4b75-8385-0628cf54d029")
                }]);


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
