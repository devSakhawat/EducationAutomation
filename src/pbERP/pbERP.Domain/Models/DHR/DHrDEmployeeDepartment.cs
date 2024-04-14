using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.DHR;

public partial class DHrDEmployeeDepartment
{
    public long EmployeeDepartmentId { get; set; }

    public string EmployeeDepartment { get; set; }

    public string EmployeeDepartmentLocal { get; set; }

    public virtual ICollection<DHrFEmployeeJobRefNo> DHrFEmployeeJobRefNos { get; set; } = new List<DHrFEmployeeJobRefNo>();
}
