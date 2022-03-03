using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.EFCore.Repositories.DataContexts
{
    public class NorthWindLogContext : DbContext
    {
        public NorthWindLogContext(DbContextOptions<NorthWindLogContext> options) : base(options) { }

        public DbSet<DomainLog> DomainLogs { get; set; }
    }
}
