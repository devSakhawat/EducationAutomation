namespace pbERP.Domain.DTOs.BSecurity;

public class BSecBUserDto
{
   public long UserId { get; set; }

   public string LoginName { get; set; }

   public string LoginNameLocal { get; set; }

   public string Password { get; set; }

   public long? UserGroupId { get; set; }

   public DateTime? StartDate { get; set; }

   public DateTime? EndDate { get; set; }

   public string? UserGroupName { get; set; }
}

