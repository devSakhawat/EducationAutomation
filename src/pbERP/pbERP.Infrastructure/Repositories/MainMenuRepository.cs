using Microsoft.EntityFrameworkCore;
using pbERP.DataStructure;
using pbERP.Domain.DTOs.Menu;
using pbERP.Domain.Models.BSecurity;
using pbERP.Infrastructure.Constracts;

namespace pbERP.Infrastructure.Repositories;

public class MainMenuRepository : GenericRepository<MainMenuDto>, IMainMenuRepository
{
   private readonly pbERPContext context;

   public MainMenuRepository(pbERPContext context) : base(context)
   {
      this.context = context;
   }

   public async Task<IReadOnlyList<MainMenuDto>> GetMainMenus(long companyId, long userId)
   {
      // this to veriable for now. it will come form login form.
      BSecBUser user = await context.BSecBUsers.Where(u => u.UserId == userId).FirstOrDefaultAsync();

      long userGroupId = await context.BSecBUsers.Where(u => u.UserId == userId).Select(u => u.UserId).FirstOrDefaultAsync();

      IReadOnlyList<MainMenuDto> mainMenus = await (from sdS in context.BSecDScreens
                                                    join seLUGS in context.BSecELinkUserGroupScreens on sdS.ScreenId equals seLUGS.ScreenId
                                                    join saUG in context.BSecAUserGroups on seLUGS.UserGroupId equals saUG.UserGroupId
                                                    join sbU in context.BSecBUsers on saUG.UserGroupId equals sbU.UserGroupId
                                                    join sciM in context.AGenConfigIModules on sdS.ModuleId equals sciM.ModuleId
                                                    join scjCLM in context.AGenConfigJCompanyLinkModules on sciM.ModuleId equals scjCLM.ModuleId
                                                    join caC in context.CCompACompanies on scjCLM.CompanyId equals caC.CompanyId
                                                    //where saUG.UserGroupId == user.UserGroupId && sdS.ParentId == 0 && caC.CompanyId == companyId
                                                    where saUG.UserGroupId == user.UserGroupId && caC.CompanyId == companyId && saUG.UserGroupId == (userGroupId == 0 ? 0 : userGroupId)
                                                    group new { sdS, sciM } by new
                                                    {
                                                       sdS.ScreenId,
                                                       sdS.ScreenName,
                                                       sdS.ControllerName,
                                                       sdS.ActionName,
                                                       sciM.ModuleId,
                                                       sdS.ParentId
                                                    } into g
                                                    orderby g.Key.ParentId, g.Key.ScreenName
                                                    select new MainMenuDto
                                                    {
                                                       ScreenId = g.Key.ScreenId,
                                                       ScreenName = g.Key.ScreenName,
                                                       ControllerName = (g.Key.ControllerName ?? ""),
                                                       ActionName = (g.Key.ActionName ?? ""),
                                                       ModuleId = g.Key.ModuleId,
                                                       ParentId = g.Key.ParentId
                                                    }).ToListAsync();
      return mainMenus;
   }
   //   SELECT a.ScreenID, a.ScreenName, IsNull(a.ControllerName,'') As ControllerName, IsNull(a.ActionName, '') As ActionName, e.ModuleID

   //WHERE e.ModuleID= 1 AND a.ParentID= 1 AND c.UserGroupID= 1 AND d.UserID= 1 AND g.companyID= 1

   public async Task<IReadOnlyList<MainMenuDto>> GetSubManus(long parentId) // SecDScreen table ScreenId
   {
      long userId = 1; // from session
      long userGroupId = await context.BSecBUsers.Where(u => u.UserId == userId).Select(u => u.UserId).FirstOrDefaultAsync();

      IReadOnlyList<MainMenuDto> subMenus = await (from sdS in context.BSecDScreens
                                                   join seLUGS in context.BSecELinkUserGroupScreens on sdS.ScreenId equals seLUGS.ScreenId
                                                   join saUG in context.BSecAUserGroups on seLUGS.UserGroupId equals saUG.UserGroupId
                                                   join sciM in context.AGenConfigIModules on sdS.ModuleId equals sciM.ModuleId
                                                   where sdS.ParentId == parentId && sdS.ParentId != 0 && saUG.UserGroupId == (userGroupId == 0 ? 0 : userGroupId)
                                                   select new MainMenuDto
                                                   {
                                                      ScreenId = sdS.ScreenId,
                                                      ScreenName = sdS.ScreenName,
                                                      ControllerName = (sdS.ControllerName ?? ""),
                                                      ActionName = (sdS.ActionName ?? ""),
                                                      ModuleId = sciM.ModuleId,
                                                      ParentId = sdS.ParentId
                                                   }).ToListAsync();
      return subMenus;
   }



}