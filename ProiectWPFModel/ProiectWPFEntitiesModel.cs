using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ProiectWPFModel
{
    public partial class ProiectWPFEntitiesModel : DbContext
    {
        public ProiectWPFEntitiesModel()
            : base("name=ProiectWPFEntitiesModel")
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .Property(e => e.PhoneC)
                .IsFixedLength();

            modelBuilder.Entity<Employee>()
                .Property(e => e.PhoneE)
                .IsFixedLength();
        }
    }
}
