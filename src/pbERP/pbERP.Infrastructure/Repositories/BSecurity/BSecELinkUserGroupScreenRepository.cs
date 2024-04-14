using Microsoft.EntityFrameworkCore;
using pbERP.DataStructure;
using pbERP.Domain.Models.BSecurity;
using pbERP.Infrastructure.Constracts.BSecurity;

namespace pbERP.Infrastructure.Repositories.BSecurity;

public class BSecELinkUserGroupScreenRepository : GenericRepository<BSecELinkUserGroupScreen>, IBSecELinkUserGroupScreenRepository
{

   public BSecELinkUserGroupScreenRepository(pbERPContext context) : base(context)
   {
   }

   public async Task<int> InsertLinkUserGroupScreen(List<BSecELinkUserGroupScreen> models)
   {
      if (models.Count() <= 0) return await Task.FromResult(0);
      int rowAffect = 0;

      using var transaction = await context.Database.BeginTransactionAsync();
      try
      {
         await context.BSecELinkUserGroupScreens.Where(x => x.UserGroupId == models.First().UserGroupId).ExecuteDeleteAsync();
         await context.BSecELinkUserGroupScreens.AddRangeAsync(models);
         rowAffect = await context.SaveChangesAsync();

         await transaction.CommitAsync();
         return rowAffect;
      }
      catch (Exception)
      {
         await transaction.RollbackAsync();
      }
      return rowAffect;
   }
}