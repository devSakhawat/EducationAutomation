using pbERP.Domain.DTOs.FEducation;
using pbERP.Domain.Models.FEducation;

namespace pbERP.Infrastructure.Constracts;

public interface IFEduOLinkClassSectionRepository : IGenericRepository<FEduOLinkClassSection>
{
  Task<int> BulkInsertLinkClassSection(List<FEduOLinkClassSectionDto> models);
}
