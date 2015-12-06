using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShiftReports.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ShiftReports.DAL
{
    public class ShiftContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Production> ProductionData { get; set; }
        public DbSet<Downtime> DowntimeData { get; set; }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<DowntimeType> DowntimeTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}