using System;
using System.Collections.Generic;

namespace pbERP.Domain.Models.BSecurity;

public partial class BSecFScreenCommand
{
    public long ScreenCommandId { get; set; }

    public string ScreenCommandName { get; set; }

    public string ScreenCommandNameLocal { get; set; }

    public long? ScreenId { get; set; }

    public string Description { get; set; }

    public virtual ICollection<BSecGLinkUserGroupScreenCommand> BSecGLinkUserGroupScreenCommands { get; set; } = new List<BSecGLinkUserGroupScreenCommand>();

    public virtual BSecDScreen Screen { get; set; }
}
