using pbERP.Domain.DTOs.FEducation;
using pbERP.Domain.Models.FEducation;

namespace pbERP.Infrastructure.Constracts.FEducation;

public interface IFEduGLinkTransportAreaRepository : IGenericRepository<FEduGLinkTransportArea>
{
   Task<int> BulkInsertLinkTransportArea(List<FEduGLinkTransportAreaDto> models);
}