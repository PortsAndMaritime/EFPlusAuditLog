using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace EFPlusAuditLog.Web.Models
{
    public class EFPContext : DbContext
    {
        public EFPContext (DbContextOptions<EFPContext> options)
            : base(options)
        {
            AuditManager.DefaultConfiguration.AutoSavePreAction = (context, audit) =>
         // ADD "Where(x => x.AuditEntryID == 0)" to allow multiple SaveChanges with same Audit
         (context as EFPContext).AuditEntries.AddRange(audit.Entries);
        }

        // ... context code ...
        public DbSet<AuditEntry> AuditEntries { get; set; }
        public DbSet<AuditEntryProperty> AuditEntryProperties { get; set; }

        public DbSet<Terminal> Terminal { get; set; }
    }
}
