using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using CashFlowAnalyzer.Shared.Models;
using Duende.IdentityServer.EntityFramework.Options;

namespace CashFlowAnalyzer.Server.Data
{
    // AuthorizationAPI provides auth + identity, in contrast to IdentityDbContext that provides only identity
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        {
        }

        public DbSet<FinancialRecordDto> FinancialRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FinancialRecordDto>(entity => {
                entity.Property(e => e.Value).HasColumnType("decimal(10,2)");
            });
        }
    }
}