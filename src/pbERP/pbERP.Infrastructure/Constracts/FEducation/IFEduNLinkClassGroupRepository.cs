using pbERP.Domain.DTOs.FEducation;
using pbERP.Domain.Models.FEducation;

namespace pbERP.Infrastructure.Constracts.FEducation;

public interface IFEduNLinkClassGroupRepository : IGenericRepository<FEduNLinkClassGroup>
{
  Task<int> BulkInsertLinkClassGroup(List<FEduNLinkClassGroupDto> models);
}
