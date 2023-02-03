using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks.Models.Domain;

namespace Tasks.Data
{
    public class MVCDemoDbContext : DbContext
    {
        public MVCDemoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> MyProperty { get; set; }
        public object Employee { get; internal set; }

        internal void ToListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
