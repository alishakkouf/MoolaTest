using BackendTask.Data.Models;
using BackendTask.Shared;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog.Events;
using BackendTask.Common;
using BackendTask.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BackendTask.Data.Models.Identity;
using System.Text.RegularExpressions;

namespace BackendTask.Data
{
    public class BackendTaskDbContext : IdentityDbContext<UserAccount, UserRole, long>
    {
        internal DbSet<CustomePermission> CustomePermissions { get; set; }
        internal DbSet<Company> Companies { get; set; }
        internal DbSet<Department> Departments { get; set; }
        internal DbSet<BankCard> BankCards { get; set; }
        internal DbSet<Transaction> Transactions { get; set; }
        internal DbSet<Attachment> Attachment { get; set; }

        private readonly ICurrentUserService _currentUserService;

        public BackendTaskDbContext(DbContextOptions<BackendTaskDbContext> options,
            ICurrentUserService currentUserService)
            : base(options)
        {
            _currentUserService = currentUserService;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var entity in builder.Model.GetEntityTypes())
            {
                // Convert table names to lowercase
                entity.SetTableName(ToSnakeCase(entity.GetTableName()));

                foreach (var property in entity.GetProperties())
                {
                    // Convert column names to lowercase
                    property.SetColumnName(ToSnakeCase(property.GetColumnName()));
                }
            }
        }

        // Helper method to convert PascalCase to snake_case
        private static string ToSnakeCase(string name)
        {
            return Regex.Replace(name, "([a-z])([A-Z])", "$1_$2").ToLower();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                if (entry.Entity is IAuditedEntity auditedEntity)
                {
                    var userId = _currentUserService.GetUserId();

                    if (entry.State == EntityState.Added)
                    {
                        auditedEntity.CreatedAt = DateTime.UtcNow;
                        auditedEntity.CreatedBy = userId;
                    }

                    if (entry.State == EntityState.Modified)
                    {
                        auditedEntity.ModifiedAt = DateTime.UtcNow;
                        auditedEntity.ModifiedBy = userId;
                    }
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}

