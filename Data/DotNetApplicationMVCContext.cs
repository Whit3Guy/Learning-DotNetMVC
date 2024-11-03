using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DotNetApplicationMVC.Models;

namespace DotNetApplicationMVC.Data
{
    public class DotNetApplicationMVCContext : DbContext
    {
        public DotNetApplicationMVCContext (DbContextOptions<DotNetApplicationMVCContext> options)
            : base(options)
        {
        }

        public DbSet<DotNetApplicationMVC.Models.Movie> Movie { get; set; } = default!;
    }
}
