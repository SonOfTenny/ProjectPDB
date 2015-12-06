namespace ShiftReports.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ShiftReports.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<ShiftReports.DAL.ShiftContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ShiftReports.DAL.ShiftContext context)
        {
            var users = new List<User>
          {
              new User { FirstMidName = "Mark", LastName = "Tennyson"},
               new User { FirstMidName = "Aaron", LastName = "Harrigan"},
                new User { FirstMidName = "Stephen", LastName = "Prunty"}
            
          };
            users.ForEach(s => context.Users.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            var plants = new List<Plant>
            {
                new Plant {Name = "Roll", MixRatePerHour = 2},
                new Plant {Name = "Batch", MixRatePerHour = 2},
                new Plant {Name = "Soda", MixRatePerHour = 2},
                new Plant {Name = "Wheaten", MixRatePerHour = 2},
                new Plant {Name = "Pan", MixRatePerHour = 2},
                new Plant {Name = "Pancakes", MixRatePerHour = 2}
         };
            plants.ForEach(s => context.Plants.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var shifts = new List<Shift>
            {
                new Shift { Name = "Day"},
                new Shift { Name = "Night"}
            };
            shifts.ForEach(s => context.Shifts.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            
        
        }
    }
}
