using AkijRest.IdentityServer.Repository.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AkijRest.IdentityServer.Repository.Helpers.DbHelpers
{
    public class IdentityServerDbContext: DbContext        
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleGroup> RoleGroups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<Token> Tokens { get; set; }
        //public DbSet<SessionExpiry> SessionExpiry { get; set; }
        
        public DbSet<ExternalLoginEmail> ExternalLoginEmails { get; set; }

        public IdentityServerDbContext()
            : base("name=AkijRest.IdentityServer")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<IdentityServerDbContext>(null);

            modelBuilder.Entity<ExternalLoginEmail>()
                .HasRequired(elm => elm.User);
        }
    }
}