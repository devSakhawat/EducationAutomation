using pbERP.Domain.Models.FEducation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pbERP.Infrastructure.Constracts.FEducation;

public interface IFEduPLinkClassShiftRepository : IGenericRepository<FEduPLinkClassShift>
{
  Task<int> BulkInsertLinkClassShift(List<FEduPLinkClassShift> models);
}
