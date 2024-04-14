using System;
using System.Collections.Generic;
using pbERP.Domain.Models.AGeneralConfig;

namespace pbERP.Domain.Models.DHR;

public partial class DHrJEmployee
{
    public long EmployeeId { get; set; }

    public long? EmployeeJobRefNoId { get; set; }

    public string EmployeeCardNo { get; set; }

    public string EmployeeName { get; set; }

    public string EmployeeNameLocal { get; set; }

    public string EmployeeFathersName { get; set; }

    public string EmployeeFathersNameLocal { get; set; }

    public long? GenderId { get; set; }

    public long? BloodGroupId { get; set; }

    public DateTime? EmployeeJoiningDate { get; set; }

    public string EmployeePhone { get; set; }

    public string EmployeeEmail { get; set; }

    public long? ReligionId { get; set; }

    public string EmployeeNationalId { get; set; }

    public string EmployeePassportNumber { get; set; }

    public int? IsActive { get; set; }

    public byte[] EmployeePhoto { get; set; }

    public string EmpPresAdd { get; set; }

    public string EmpPresAddLocal { get; set; }

    public long? EmpPresAddPsid { get; set; }

    public string EmpPerAdd { get; set; }

    public string EmpPerAddInLocal { get; set; }

    public long? EmpPerAddPsid { get; set; }

    public virtual AGenConfigFBloodGroup BloodGroup { get; set; }

    public virtual AGenConfigDPoliceStation EmpPresAddPs { get; set; }

    public virtual DHrFEmployeeJobRefNo EmployeeJobRefNo { get; set; }

    public virtual AGenConfigEGender Gender { get; set; }

    public virtual AGenConfigGReligion Religion { get; set; }
}
