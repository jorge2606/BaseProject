using System;
using System.Collections.Generic;
using System.Text;

namespace Audit.Data
{
    public class Audit_User : AuditBase
    {
        public int Dni { set; get; }
        public string UserName { set; get; }
        public Guid UserId { set; get; }
    }
}
