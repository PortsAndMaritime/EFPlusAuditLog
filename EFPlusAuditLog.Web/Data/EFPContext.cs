using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFPlusAuditLog.Web.Models
{
    public class EFPContext : DbContext
    {
        public EFPContext (DbContextOptions<EFPContext> options)
            : base(options)
        {
        }

        public DbSet<EFPlusAuditLog.Web.Models.Terminal> Terminal { get; set; }
    }
}
