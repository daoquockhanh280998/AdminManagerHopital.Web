using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SwaggerAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SwaggerAPI.Data.EF
{
    public class DemoAPIContext : DbContext
    {
        //   public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }

        public IConfiguration Configuration { get; }

        public DemoAPIContext(DbContextOptions<DemoAPIContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(Configuration["ConnectionStrings:DBContext"]);
    }
}