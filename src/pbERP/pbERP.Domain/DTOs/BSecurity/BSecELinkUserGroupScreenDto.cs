namespace pbERP.Domain.DTOs.BSecurity;

public class BSecELinkUserGroupScreenDto
{
   public long LinkUserGroupScreenId { get; set; }

   public long? UserGroupId { get; set; }

   public long? ScreenId { get; set; }

   public string? ScreenName { get; set; }

   public string? UserGroupName { get; set; }
}
