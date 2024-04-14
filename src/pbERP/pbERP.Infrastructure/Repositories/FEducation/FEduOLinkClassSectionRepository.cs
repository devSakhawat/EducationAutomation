using Microsoft.EntityFrameworkCore;
using pbERP.DataStructure;
using pbERP.Domain.DTOs.FEducation;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts;

namespace pbERP.Infrastructure.Repositories.FEducation;

public class FEduOLinkClassSectionRepository : GenericRepository<FEduOLinkClassSection>, IFEduOLinkClassSectionRepository
{
  public FEduOLinkClassSectionRepository(pbERPContext context) : base(context)
  {
  }


  public async Task<int> BulkInsertLinkClassSection(List<FEduOLinkClassSectionDto> models)
  {
    if (models.Count() <= 0) return await Task.FromResult(0);
    List<FEduOLinkClassSection> records = await context.FEduOLinkClassSections.Where(x => x.ClassId == models.First().ClassId).ToListAsync();
    if (records.Count > 0)
    {
      context.FEduOLinkClassSections.RemoveRange(records);
      await context.SaveChangesAsync();
    }

      if (models.Count == 1)
      {
         models.Select(x => x.ClassSectionId = null).ToList();
      }

      long id = await context.FEduOLinkClassSections.MaxAsync(e => (long?)e.LinkClassSectionId) ?? 0;

      //long id = await context.FEduOLinkClassSections.MaxAsync(x => x.LinkClassSectionId);
    List<FEduOLinkClassSection> objs = models.Select(x => new FEduOLinkClassSection
    { LinkClassSectionId = ++id, ClassId = x.ClassId, ClassSectionId = x.ClassSectionId }).ToList();

    await context.FEduOLinkClassSections.AddRangeAsync(objs);

    return (await context.SaveChangesAsync() > 0) ? 200 : 400;
  }
}
