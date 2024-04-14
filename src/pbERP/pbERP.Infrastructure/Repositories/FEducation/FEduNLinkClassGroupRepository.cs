using Microsoft.EntityFrameworkCore;
using pbERP.DataStructure;
using pbERP.Domain.DTOs.FEducation;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts.FEducation;

namespace pbERP.Infrastructure.Repositories.FEducation;

public class FEduNLinkClassGroupRepository : GenericRepository<FEduNLinkClassGroup>, IFEduNLinkClassGroupRepository
{
   public FEduNLinkClassGroupRepository(pbERPContext context) : base(context)
   {
   }

   public async Task<int> BulkInsertLinkClassGroup(List<FEduNLinkClassGroupDto> models)
   {
      if (models.Count() <= 0) return await Task.FromResult(0);
      List<FEduNLinkClassGroup> records = await context.FEduNLinkClassGroups.Where(x => x.ClassId == models.First().ClassId).ToListAsync();
      if (records.Count > 0)
      {
         context.FEduNLinkClassGroups.RemoveRange(records);
         await context.SaveChangesAsync();
      }
      if (models.Count == 1)
      {
         models.Select(x => x.ClassGroupId = null);
      }

      long id = await context.FEduNLinkClassGroups.MaxAsync(x => (long?)x.LinkClassGroupId) ?? 0;

      List<FEduNLinkClassGroup> objs = models.Select(x => new FEduNLinkClassGroup
      { 
         LinkClassGroupId = ++id, 
         ClassId = x.ClassId, 
         ClassGroupId = x.ClassGroupId 
      }).ToList();

      await context.FEduNLinkClassGroups.AddRangeAsync(objs);

      return (await context.SaveChangesAsync() > 0) ? 200 : 400;     
   }
}
