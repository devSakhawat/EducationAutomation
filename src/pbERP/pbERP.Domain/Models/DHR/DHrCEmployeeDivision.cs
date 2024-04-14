using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.DHR;

public partial class DHrCEmployeeDivision
{
    public long EmployeeDivisionId { get; set; }

    public string EmployeeDivision { get; set; }

    public string EmployeeDivisionLocal { get; set; }

    public virtual ICollection<DHrFEmployeeJobRefNo> DHrFEmployeeJobRefNos { get; set; } = new List<DHrFEmployeeJobRefNo>();
}
