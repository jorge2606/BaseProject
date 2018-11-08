using System;

namespace Audit.Data
{
    public interface IAudit
    {
       string AuditAction { get; set; }
       DateTime AuditDate { get; set; }
       string AuditUser { get; set; }
    }
}   
