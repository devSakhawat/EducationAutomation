using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.BSecurity;

public partial class BSecAUserGroup
{
    public long UserGroupId { get; set; }

    public string UserGroupName { get; set; }

    public string UserGroupNameLocal { get; set; }

    public string UserGroupDescription { get; set; }

    public string UserGroupDescriptionLocal { get; set; }

    public double? UserGroupSaleDiscount { get; set; }

    public virtual ICollection<BSecBUser> BSecBUsers { get; set; } = new List<BSecBUser>();

    public virtual ICollection<BSecELinkUserGroupScreen> BSecELinkUserGroupScreens { get; set; } = new List<BSecELinkUserGroupScreen>();

    public virtual ICollection<BSecGLinkUserGroupScreenCommand> BSecGLinkUserGroupScreenCommands { get; set; } = new List<BSecGLinkUserGroupScreenCommand>();
}
