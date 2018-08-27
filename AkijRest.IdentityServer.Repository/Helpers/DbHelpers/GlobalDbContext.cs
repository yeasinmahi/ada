using AkijRest.IdentityServer.Repository.Models.Global;

namespace AkijRest.IdentityServer.Repository.Helpers.DbHelpers
{
    using System.Data.Entity;

    public partial class GlobalDbContext : DbContext
    {
        public GlobalDbContext()
            : base("GlobalDbContext")
        {
        }

        public virtual DbSet<tblTaskRecord> tblTaskRecords { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblTaskRecord>()
                .Property(e => e.strKeyPoint)
                .IsUnicode(false);

            modelBuilder.Entity<tblTaskRecord>()
                .Property(e => e.strStatus)
                .IsUnicode(false);

            modelBuilder.Entity<tblTaskRecord>()
                .Property(e => e.strRemarks)
                .IsUnicode(false);

            modelBuilder.Entity<tblTaskRecord>()
                .Property(e => e.strType)
                .IsUnicode(false);
        }
    }
}
