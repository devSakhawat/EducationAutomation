using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.DHR;

public partial class DHrFEmployeeJobRefNo
{
    public long EmployeeJobRefNoId { get; set; }

    public string EmployeeJobRefNo { get; set; }

    public long? EmployeeDivisionId { get; set; }

    public long? EmployeeDepartmentId { get; set; }

    public long? EmployeeDesignationId { get; set; }

    public long? EmployeeCategoryId { get; set; }

    public virtual ICollection<DHrJEmployee> DHrJEmployees { get; set; } = new List<DHrJEmployee>();

    public virtual DHrBEmployeeCategory EmployeeCategory { get; set; }

    public virtual DHrDEmployeeDepartment EmployeeDepartment { get; set; }

    public virtual DHrEEmployeeDesignation EmployeeDesignation { get; set; }

    public virtual DHrCEmployeeDivision EmployeeDivision { get; set; }
}
