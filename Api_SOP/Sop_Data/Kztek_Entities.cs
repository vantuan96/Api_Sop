using System;
using System.Threading.Tasks;
using Api_SOP.Sop_model;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
//using MySql.Data.MySqlClient;

namespace Sop_Data
{
    public class Kztek_Entities : DbContext
    {
        public Kztek_Entities(DbContextOptions<Kztek_Entities> options) : base(options)
        {

        }

        //Main
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {

            });
        }


    }
}
