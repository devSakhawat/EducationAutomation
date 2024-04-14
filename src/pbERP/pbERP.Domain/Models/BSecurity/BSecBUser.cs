using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.BSecurity;

public partial class BSecBUser
{
    public long UserId { get; set; }

    public string LoginName { get; set; }

    public string LoginNameLocal { get; set; }

    public string Password { get; set; }

    public long? UserGroupId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual BSecAUserGroup UserGroup { get; set; }
}
