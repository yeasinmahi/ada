using System.Data.Entity;
using AkijRest.IdentityServer.Repository.Models.Hr;

namespace AkijRest.IdentityServer.Repository.Helpers.DbHelpers
{
    public partial class HrDbContext : DbContext
    {
        public HrDbContext()
            : base("HrDbContext")
        {
        }

        public virtual DbSet<tblCafeteria> tblCafeterias { get; set; }
        public virtual DbSet<tblCafeteriaDetail> tblCafeteriaDetails { get; set; }
        public virtual DbSet<tblCafeteriaRate> tblCafeteriaRates { get; set; }
        public virtual DbSet<tblDay> tblDays { get; set; }
        public virtual DbSet<tblEmployeeAttendance> tblEmployeeAttendances { get; set; }

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
            modelBuilder.Entity<tblEmployeeAttendance>()
                .Property(e => e.strEmployeeBarcode)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployeeAttendance>()
                .Property(e => e.strUserIP)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmployeeAttendance>()
                .Property(e => e.strRemark)
                .IsUnicode(false);
        }
    }
}
