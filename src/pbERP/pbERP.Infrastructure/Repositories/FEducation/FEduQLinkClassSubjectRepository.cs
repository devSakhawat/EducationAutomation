using Microsoft.EntityFrameworkCore;
using pbERP.DataStructure;
using pbERP.Domain.DTOs.FEducation;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts.FEducation;

namespace pbERP.Infrastructure.Repositories.FEducation;

public class FEduQLinkClassSubjectRepository : GenericRepository<FEduQLinkClassSubject>, IFEduQLinkClassSubjectRepository
{
   public FEduQLinkClassSubjectRepository(pbERPContext context) : base(context)
   {
   }

   public async Task<int> BulkInsertLinkClassSubject(List<FEduQLinkClassSubjectDto> models)
   {
      if (models.Count() <= 0) return await Task.FromResult(0);

      List<FEduQLinkClassSubject> records =
        await context.FEduQLinkClassSubjects.Where(x => x.ClassId == models.First().ClassId).ToListAsync();

      if (records.Count() > 0)
      {
         context.FEduQLinkClassSubjects.RemoveRange(records);
         await context.SaveChangesAsync();
      }

      if(models.Count == 1)
      {
         models.Select(x => x.ClassSubjectId = null).ToList();
      }

      long id =  context.FEduQLinkClassSubjects.Max(e => (long?)e.LinkClassSubjectId) ?? 0;
      
      List<FEduQLinkClassSubject> objs = models.Select(x => new FEduQLinkClassSubject
      {
         LinkClassSubjectId = ++id,
         ClassId = x.ClassId,
         ClassGroupId = x.ClassGroupId,
         ClassSubjectId = x.ClassSubjectId
      }).ToList();

      await context.FEduQLinkClassSubjects.AddRangeAsync(objs);
      return (await context.SaveChangesAsync() < 0) ? 400 : 200;
   }
}
