using DEMO.DAL.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMO.DAL.Context
{
    public class Add_context :/*DbContext*/ IdentityDbContext<AplicationUser, ApplicationRole,string>
    {
        public Add_context(DbContextOptions<Add_context> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            // optionsBuilder.UseSqlServer("server=.; database=company_mina; integrated security=true;");
        }


        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Department { get; set; }

    }
}
