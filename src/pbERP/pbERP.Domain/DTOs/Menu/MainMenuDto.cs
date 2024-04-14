namespace pbERP.Domain.DTOs.Menu;

public class MainMenuDto
{
   public long ScreenId { get; set; }

   public string? ScreenName { get; set; }

   public string? ControllerName { get; set; }

   public string? ActionName { get; set; }

   public long ModuleId { get; set; }

   public long? ParentId { get; set; }
}
