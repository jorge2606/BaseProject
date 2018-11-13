class UserAuditBase{
    dni : number;
    userName : string;
}
export class UserAudit{
    auditAction : string;
    auditDate : Date;
    auditUser : string;
    id : number;
    userName : string;
    current : UserAuditBase;
    previous : UserAuditBase;
}