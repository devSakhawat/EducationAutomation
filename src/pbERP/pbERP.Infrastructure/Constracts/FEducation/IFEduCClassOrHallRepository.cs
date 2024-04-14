using pbERP.Domain.DTOs.FEducation;
using pbERP.Domain.Models.FEducation;

namespace pbERP.Infrastructure.Constracts.FEducation;

public interface IFEduCClassOrHallRepository : IGenericRepository<FEduCClassOrHall>
{
  Task<IReadOnlyList<FEduCClassOrHallDto>> GetClassOrHallsAsync();
}
