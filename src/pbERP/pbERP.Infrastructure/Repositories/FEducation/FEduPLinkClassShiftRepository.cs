using Microsoft.EntityFrameworkCore;
using pbERP.DataStructure;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts.FEducation;

namespace pbERP.Infrastructure.Repositories.FEducation;

public class FEduPLinkClassShiftRepository : GenericRepository<FEduPLinkClassShift>, IFEduPLinkClassShiftRepository
{
   public FEduPLinkClassShiftRepository(pbERPContext _context) : base(_context)
   {
   }

   public async Task<int> BulkInsertLinkClassShift(List<FEduPLinkClassShift> models)
   {
      if (models.Count() <= 0) return await Task.FromResult(0);
      List<FEduPLinkClassShift> records =
        await context.FEduPLinkClassShifts.Where(x => x.ClassId == models.First().ClassId).ToListAsync();

      if (records.Count() > 0)
      {
         context.FEduPLinkClassShifts.RemoveRange(records);
         await context.SaveChangesAsync();
      }

      if (models.Count == 1)
      {
         models.Select(x => x.ClassShiftId = null);
      }

      long id = await context.FEduPLinkClassShifts.MaxAsync(x => (long?)x.LinkClassShiftId) ?? 0;
      
      List<FEduPLinkClassShift> objs = models.Select(x => new FEduPLinkClassShift
      {
         LinkClassShiftId = ++id,
         ClassId = x.ClassId,
         ClassShiftId = x.ClassShiftId
      }).ToList();

      await context.FEduPLinkClassShifts.AddRangeAsync(objs);
      return (await context.SaveChangesAsync() > 0) ? 200 : 400;
   }
}
