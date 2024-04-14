using System;
using System.Collections.Generic;
using pbERP.Domain.Models.AGeneralConfig;

namespace pbERP.Domain.Models.BSecurity;

public partial class BSecDScreen
{
    public long ScreenId { get; set; }

    public string ScreenName { get; set; }

    public string ScreenNameInLocal { get; set; }

    public string ControllerName { get; set; }

    public string ActionName { get; set; }

    public long? ModuleId { get; set; }

    public long? ParentId { get; set; }

    public virtual ICollection<BSecELinkUserGroupScreen> BSecELinkUserGroupScreens { get; set; } = new List<BSecELinkUserGroupScreen>();

    public virtual ICollection<BSecFScreenCommand> BSecFScreenCommands { get; set; } = new List<BSecFScreenCommand>();

    public virtual AGenConfigIModule Module { get; set; }
}
