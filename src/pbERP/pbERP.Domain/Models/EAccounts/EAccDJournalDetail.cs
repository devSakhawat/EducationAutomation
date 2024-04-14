using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.EAccounts;

public partial class EAccDJournalDetail
{
    public long JournalDetailsId { get; set; }

    public string JournalCode { get; set; }

    public long? ChartofAccountId { get; set; }

    public string Description { get; set; }

    public decimal? Debit { get; set; }

    public decimal? Credit { get; set; }

    public string Reference { get; set; }

    public long? TrChartOfAccountId { get; set; }

    public long? UserId { get; set; }

    public DateTime? Udate { get; set; }

    public DateTime? TrDt { get; set; }

    public long? FinancialYearId { get; set; }

    public long? BranchId { get; set; }

    public int? IsPosted { get; set; }

    public int? TransFrom { get; set; }

    public string BoothCode { get; set; }

    public long? ProjectId { get; set; }

    public long? CompanyId { get; set; }

    public int? IsCollection { get; set; }

    public long? SalesType { get; set; }

    public virtual EAccBChartOfAccount ChartofAccount { get; set; }

    public virtual EAccCJournalMaster JournalCodeNavigation { get; set; }
}
