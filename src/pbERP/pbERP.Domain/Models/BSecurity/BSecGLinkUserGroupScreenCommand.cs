using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.BSecurity;

public partial class BSecGLinkUserGroupScreenCommand
{
    public long LinkUserGroupScreenCommandId { get; set; }

    public long? UserGroupId { get; set; }

    public long? ScreenCommandId { get; set; }

    public virtual BSecFScreenCommand ScreenCommand { get; set; }

    public virtual BSecAUserGroup UserGroup { get; set; }
}
