using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BT2.Models
{
    {
    public partial class LTQLDbContext : DbContext
    {
        public LTQLDbContext()
            : base("name=LTQLDbContext")
        {
        }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<AccountModel> AccountModels { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .Property(e => e.PersonID)
                .IsUnicode(false);
            modelBuilder.Entity<Person>()
                .Property(e => e.PersonName)
                .IsUnicode(false);
        }

    }
}
