namespace pbERP.Domain.DTOs.BSecurity;

public class BSecAUserGroupDto
{
   public long UserGroupId { get; set; }

   public string UserGroupName { get; set; }

   public string? UserGroupNameLocal { get; set; }

   public string? UserGroupDescription { get; set; }

   public string? UserGroupDescriptionLocal { get; set; }

   public double? UserGroupSaleDiscount { get; set; }
}

