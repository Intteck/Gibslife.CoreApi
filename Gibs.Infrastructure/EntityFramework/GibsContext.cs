using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Gibs.Domain.Entities;

namespace Gibs.Infrastructure
{
    public partial class GibsContext(DbContextOptions<GibsContext> options) : DbContext(options)
    {
        private static User SystemAccount => 
            new("SYSTEM", "root", "System", "Account", "2348099999247", "system@gibsonline.com");

        public Func<User> GetLogonUser { get; set; } = () => SystemAccount;

        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            base.ConfigureConventions(builder);

            //change all enum converters to string
            builder.Properties<Enum>().HaveConversion<string>();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(GibsContext).Assembly);

            // MOVE TO ENTITY CONFIG FILES
            builder.Entity<AutoNumber>()
                   .HasKey(x => new { x.NumType, x.BranchID });

            builder.Entity<PolicyAutoNumber>()
                   .HasKey(x => new { x.NumType, x.RiskID, x.BranchID });

            //remove all cascade deletes found.
            builder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership)
                .Where(fk => fk.DeleteBehavior == DeleteBehavior.Cascade)
                .ToList()
                .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.LogTo(message => System.Diagnostics.Debug.WriteLine(message));
            builder.EnableDetailedErrors();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (GetLogonUser == null)
                throw new InvalidOperationException("GetLogonUser() delegate not assigned in GibsContext");

            var user = GetLogonUser.Invoke();

            if (string.IsNullOrEmpty(user.Id))
                throw new InvalidOperationException(nameof(user.Id));

            var items = ChangeTracker.Entries<AuditRecord>()
                                     .Where(x => x.Entity is not null) 
                                     .Where(x => x.State == EntityState.Added
                                              || x.State == EntityState.Modified);
            foreach (var item in items)
            {
                if (item.State == EntityState.Added)
                {
                    item.Property(x => x.CreatedBy).CurrentValue = user.Id;
                    item.Property(x => x.CreatedUtc).CurrentValue = DateTime.UtcNow;
                    item.Property(x => x.LastModifiedBy).CurrentValue = null;
                    item.Property(x => x.LastModifiedUtc).CurrentValue = null;
                }
                else if (item.State == EntityState.Modified)
                {
                    item.Property(x => x.LastModifiedBy).CurrentValue = user.Id;
                    item.Property(x => x.LastModifiedUtc).CurrentValue = DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(true, default);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return SaveChangesAsync(cancellationToken);
        }

        #region Disable SaveChanges
        [Obsolete("Please call SaveChangesAsync()", error: true)]
        public override int SaveChanges()
        {
            throw new NotImplementedException("SaveChanges() is disabled");
        }

        [Obsolete("Please call SaveChangesAsync()", error: true)]
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            throw new NotImplementedException("SaveChanges() is disabled");
        }
        #endregion
    }
}