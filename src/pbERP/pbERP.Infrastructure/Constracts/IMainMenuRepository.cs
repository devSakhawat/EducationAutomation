using pbERP.Domain.DTOs.Menu;

namespace pbERP.Infrastructure.Constracts;

public interface IMainMenuRepository : IGenericRepository<MainMenuDto>
{
   Task<IReadOnlyList<MainMenuDto>> GetMainMenus(long companyId, long userId);
   Task<IReadOnlyList<MainMenuDto>> GetSubManus(long parentId);
}
