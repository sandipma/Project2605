using BaseModels.ApplicationUser;
using BaseModels.Employee;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDataAccess
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DbContextClass(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
            base.OnConfiguring(options);
           // options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), options => options.EnableRetryOnFailure());
        }

        public DbSet<ApplicationUserInsert> User { get; set; }

        public DbSet<EmployeeInsert> EmployeeInsert { get; set; }

        public DbSet<EmplyeeData> EmplyoeeData { get; set; }

        public DbSet<ApplicationUserDt> UserData { get; set; }
    }
}
