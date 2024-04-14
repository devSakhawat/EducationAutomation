using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.BSecurity;

public partial class BSecHUserLoginTracking
{
    public long UserLoginTrackingId { get; set; }

    public long? UserId { get; set; }

    public string ComputerName { get; set; }

    public DateTime? LoginDate { get; set; }

    public DateTime? LoginTime { get; set; }

    public string ActionType { get; set; }
}
