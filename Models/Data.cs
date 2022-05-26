using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShipmentTracking.Models;

namespace ShipmentTracking.Models
{
    public class Data :DbContext 
    {
        //const bool USE_SEQUENCE_ID = true; // Squenced or random inc for identify column.

        public Data() : base()
        {

        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer(Program.ConnectionString);
            optionsBuilder.EnableSensitiveDataLogging(true);
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Customers> Customers { get; set; }

        public DbSet<Shipments> Shipments { get; set; }

        public DbSet<Users> Users { get; set; }
    }
}
