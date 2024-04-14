using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.DHR;

public partial class DHrBEmployeeCategory
{
    public long EmployeeCategoryId { get; set; }

    public string EmployeeCategory { get; set; }

    public string EmployeeCategoryLocal { get; set; }

    public string EmployeeGrade { get; set; }

    public string EmployeeGradeLocal { get; set; }

    public virtual ICollection<DHrFEmployeeJobRefNo> DHrFEmployeeJobRefNos { get; set; } = new List<DHrFEmployeeJobRefNo>();
}
