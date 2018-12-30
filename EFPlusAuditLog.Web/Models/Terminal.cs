using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFPlusAuditLog.Web.Models
{
    public class Terminal
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public virtual ICollection<Branch> Branches { get; set; }
    }

    public class Branch
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        public int TerminalId { get; set; }

        public Terminal Terminal { get; set; }
    }
}
