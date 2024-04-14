using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.DHR;

public partial class DHrEEmployeeDesignation
{
    public long EmployeeDesignationId { get; set; }

    public string EmployeeDesignation { get; set; }

    public string EmployeeDesignationLoacl { get; set; }

    public virtual ICollection<DHrFEmployeeJobRefNo> DHrFEmployeeJobRefNos { get; set; } = new List<DHrFEmployeeJobRefNo>();
}
