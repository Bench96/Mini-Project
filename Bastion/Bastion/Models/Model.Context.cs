﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bastion.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GameRentalsEntities : DbContext
    {
        public GameRentalsEntities()
            : base("name=GameRentalsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Developer> Developers { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Rental> Rentals { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
    }
}
