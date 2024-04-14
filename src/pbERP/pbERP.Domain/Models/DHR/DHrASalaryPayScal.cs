using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.DHR;

public partial class DHrASalaryPayScal
{
    public long SalaryPayScalId { get; set; }

    public decimal? BasicSalary { get; set; }

    public decimal? HouseRentPercent { get; set; }

    public decimal? HouseRent { get; set; }

    public decimal? ConveyancePercent { get; set; }

    public decimal? ConveyanceAllowance { get; set; }

    public decimal? MedicalPercent { get; set; }

    public decimal? MedicalAllowance { get; set; }

    public decimal? FoodPercent { get; set; }

    public decimal? FoodAllowance { get; set; }

    public decimal? CompanyProvidentPercent { get; set; }

    public decimal? EmployeeProvidentFundPercent { get; set; }

    public decimal? KallanTahabilPercent { get; set; }

    public long? CompanyId { get; set; }

    public decimal? TotalSalary { get; set; }
}
