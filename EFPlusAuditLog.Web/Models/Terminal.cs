using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFPlusAuditLog.Web.Models
{
    public class Terminal
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }
}
