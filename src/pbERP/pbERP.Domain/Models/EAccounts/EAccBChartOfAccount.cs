using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.EAccounts;

public partial class EAccBChartOfAccount
{
    public long ChartOfAccountId { get; set; }

    public string ChartOfAccountNo { get; set; }

    public string ChartOfAccountNameIn { get; set; }

    public string ChartOfAccountNameInLocal { get; set; }

    public int? ChartOfAccountNature { get; set; }

    public decimal? ChartOfAccountInitialBalance { get; set; }

    public decimal? ChartOfAccountReserveAmount { get; set; }

    public int? ReserveAmountForTheMonthOf { get; set; }

    public string ReserveAmountForTheFinancialYearOf { get; set; }

    public long? UserId { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public long? ChartOfAccountParentId { get; set; }

    public int? HierarchyLevel { get; set; }

    public virtual ICollection<EAccDJournalDetail> EAccDJournalDetails { get; set; } = new List<EAccDJournalDetail>();
}
