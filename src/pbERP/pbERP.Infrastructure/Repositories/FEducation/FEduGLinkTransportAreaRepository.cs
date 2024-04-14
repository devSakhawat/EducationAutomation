using Microsoft.EntityFrameworkCore;
using pbERP.DataStructure;
using pbERP.Domain.DTOs.FEducation;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts.FEducation;

namespace pbERP.Infrastructure.Repositories.FEducation;

public class FEduGLinkTransportAreaRepository : GenericRepository<FEduGLinkTransportArea>, IFEduGLinkTransportAreaRepository
{
  public FEduGLinkTransportAreaRepository(pbERPContext context) : base(context)
  {
  }

  public async Task<int> BulkInsertLinkTransportArea(List<FEduGLinkTransportAreaDto> models)
  {
    if (models.Count() <= 0) return await Task.FromResult(0);
    List<FEduGLinkTransportArea> records = await context.FEduGLinkTransportAreas.Where(x => x.TransportId == models.First().TransportId).ToListAsync();
    if(records.Count > 0)
    {
      context.FEduGLinkTransportAreas.RemoveRange(records);
      await context.SaveChangesAsync();
    }

    long id = await context.FEduGLinkTransportAreas.MaxAsync(x => x.LinkAreaTransportId);
    List<FEduGLinkTransportArea> linkTransportAreas = models.Select(x => new FEduGLinkTransportArea
    { LinkAreaTransportId = ++id, TransportId = x.TransportId, TransportAreaId = x.TransportAreaId }).ToList();

    await context.FEduGLinkTransportAreas.AddRangeAsync(linkTransportAreas);

    return (await context.SaveChangesAsync() <= 0) ? 400  : 200;
  }
}
