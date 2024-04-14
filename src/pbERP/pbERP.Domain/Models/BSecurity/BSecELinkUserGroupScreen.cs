using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.BSecurity;

public partial class BSecELinkUserGroupScreen
{
    public long LinkUserGroupScreenId { get; set; }

    public long UserGroupId { get; set; }

    public long? ScreenId { get; set; }

    public virtual BSecDScreen Screen { get; set; }

    public virtual BSecAUserGroup UserGroup { get; set; }
}
