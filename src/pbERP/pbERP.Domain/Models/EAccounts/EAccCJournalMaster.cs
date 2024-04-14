using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.EAccounts;

public partial class EAccCJournalMaster
{
    public long JournalMasterId { get; set; }

    public string JournalCode { get; set; }

    public DateTime? PostingDate { get; set; }

    public decimal? PostingBy { get; set; }

    public decimal? FinancialYearId { get; set; }

    public decimal? Amount { get; set; }

    public string AmountReference { get; set; }

    public DateTime? JournalDate { get; set; }

    public decimal? UserId { get; set; }

    public DateTime? Udate { get; set; }

    public DateTime? Udt { get; set; }

    public string VoucherDirection { get; set; }

    public long? BranchId { get; set; }

    public long? IsPosted { get; set; }

    public int? TransFrom { get; set; }

    public string BoothCode { get; set; }

    public long? ProjectId { get; set; }

    public long? CompanyId { get; set; }

    public int? IsCollection { get; set; }

    public long? SalesType { get; set; }

    public virtual ICollection<EAccDJournalDetail> EAccDJournalDetails { get; set; } = new List<EAccDJournalDetail>();
}
