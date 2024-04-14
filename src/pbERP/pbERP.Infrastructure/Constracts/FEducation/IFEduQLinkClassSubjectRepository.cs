using pbERP.Domain.DTOs.FEducation;
using pbERP.Domain.Models.FEducation;

namespace pbERP.Infrastructure.Constracts.FEducation;

public interface IFEduQLinkClassSubjectRepository : IGenericRepository<FEduQLinkClassSubject>
{
   Task<int> BulkInsertLinkClassSubject(List<FEduQLinkClassSubjectDto> models);
}
