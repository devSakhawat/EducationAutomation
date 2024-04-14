namespace pbERP.Domain.DTOs.BSecurity;

public class BSecDScreenDto
{
   public long ScreenId { get; set; }

   public string ScreenName { get; set; }

   public string? ScreenNameInLocal { get; set; }

   public string? ControllerName { get; set; }

   public string? ActionName { get; set; }

   public long? ModuleId { get; set; }

   public long? ParentId { get; set; }

   public string ModuleName { get; set; }

   public string ParentName { get; set; }
}
