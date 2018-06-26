using System.Data.Entity;
using AkijRest.IdentityServer.Repository.Models.Hr;

namespace AkijRest.IdentityServer.Repository.Helpers.DbHelpers
{
    public partial class HrDbContext : DbContext
    {
        public HrDbContext()
            : base("name=HrDbContext")
        {
        }

        public virtual DbSet<tblCafeteria> tblCafeterias { get; set; }
        public virtual DbSet<tblCafeteriaDetail> tblCafeteriaDetails { get; set; }
        public virtual DbSet<tblCafeteriaRate> tblCafeteriaRates { get; set; }
        public virtual DbSet<tblDay> tblDays { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblCafeteria>()
                .Property(e => e.strNarration)
                .IsUnicode(false);

            modelBuilder.Entity<tblCafeteriaDetail>()
                .Property(e => e.strNarration)
                .IsUnicode(false);

            modelBuilder.Entity<tblCafeteriaRate>()
                .Property(e => e.strGroup)
                .IsUnicode(false);

            modelBuilder.Entity<tblCafeteriaRate>()
                .Property(e => e.strNarration)
                .IsUnicode(false);

            modelBuilder.Entity<tblDay>()
                .Property(e => e.strDayName)
                .IsUnicode(false);

            modelBuilder.Entity<tblDay>()
                .Property(e => e.strMenuList)
                .IsUnicode(false);

            modelBuilder.Entity<tblDay>()
                .Property(e => e.strMenuList0)
                .IsUnicode(false);
        }
    }
}
