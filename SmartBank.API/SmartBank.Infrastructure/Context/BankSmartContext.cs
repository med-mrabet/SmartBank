using Microsoft.EntityFrameworkCore;
using SmartBank.Domain.Entities;
using SmartBank.Domain.Entities.BaseEntity;


namespace SmartBank.Infrastructure.Context
{
    public class BankSmartContext : DbContext
    {
        public BankSmartContext(DbContextOptions<BankSmartContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply all configurations from the current assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BankSmartContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        // Define DbSet properties for your entities

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
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
            return base.SaveChangesAsync(cancellationToken);
        }
   
    }
}
