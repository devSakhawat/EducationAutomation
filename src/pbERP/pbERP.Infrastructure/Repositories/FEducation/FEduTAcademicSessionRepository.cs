﻿using pbERP.DataStructure;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts.FEducation;

namespace pbERP.Infrastructure.Repositories.FEducation;

public class FEduTAcademicSessionRepository : GenericRepository<FEduTAcademicSession>, IFEduTAcademicSessionRepository
{
   public FEduTAcademicSessionRepository(pbERPContext context) : base(context)
   {
   }
}
