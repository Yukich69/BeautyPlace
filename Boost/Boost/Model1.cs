using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Boost
{
    public partial class Model1 : DbContext
    {
        public Model1(): base("name=Model1")
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }

        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Visits)
                .WithOptional(e => e.Client)
                .HasForeignKey(e => e.Client_Id);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Visits)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.Employee_Id);

            modelBuilder.Entity<Group>()
                .Property(e => e.NameGroup)
                .IsUnicode(false);

            modelBuilder.Entity<Group>()
                .HasMany(e => e.Positions)
                .WithOptional(e => e.Group)
                .HasForeignKey(e => e.Group_Id);

            modelBuilder.Entity<Group>()
                .HasMany(e => e.Services)
                .WithOptional(e => e.Group)
                .HasForeignKey(e => e.Group_Id);

            modelBuilder.Entity<Position>()
                .Property(e => e.NamePositions)
                .IsUnicode(false);

            modelBuilder.Entity<Position>()
                .HasMany(e => e.Employees)
                .WithOptional(e => e.Position)
                .HasForeignKey(e => e.Positions_Id);

            modelBuilder.Entity<Service>()
                .Property(e => e.NameService)
                .IsUnicode(false);

            modelBuilder.Entity<Service>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Service>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.Visits)
                .WithOptional(e => e.Service)
                .HasForeignKey(e => e.Service_Id);
        }
    }
}
