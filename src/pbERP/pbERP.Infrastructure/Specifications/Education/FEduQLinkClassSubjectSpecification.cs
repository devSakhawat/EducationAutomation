using pbERP.Domain.Models.FEducation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pbERP.Infrastructure.Specifications.Education;


public class FEduQLinkClassSubjectSpecification : BaseSpecification<FEduQLinkClassSubject>
{
   public FEduQLinkClassSubjectSpecification(SpecificationParams specParams)
     : base(x => string.IsNullOrEmpty(specParams.Search) || x.LinkClassSubjectId.ToString().ToLower().Contains(specParams.Search))
   {
      AddInclude(x => x.Class);
      AddInclude(x => x.ClassGroup);
      AddInclude(x => x.ClassSubject);

      if (!string.IsNullOrEmpty(specParams.Sort))
      {
         switch (specParams.Sort)
         {
            case "nameAsc": AddOrderBy(x => x.ClassId); break;
            case "nameDesc": AddOrderByDescending(x => x.ClassId); break;
            default: AddOrderBy(x => x.LinkClassSubjectId); break;
         }
      }
   }

   public FEduQLinkClassSubjectSpecification(long id) : base(x => x.LinkClassSubjectId == id)
   {
      AddInclude(x => x.Class);
      AddInclude(x => x.ClassGroup);
      AddInclude(x => x.ClassSubject);
   }
}


public class FEduQLinkClassSubjectCount : BaseSpecification<FEduQLinkClassSubject>
{
   public FEduQLinkClassSubjectCount(SpecificationParams specParams)
     : base(x => string.IsNullOrEmpty(specParams.Search) || x.ClassId.ToString().ToLower().Contains(specParams.Search))
   {

   }
}

public class FEduQLinkClassSubjectDelete : BaseSpecification<FEduQLinkClassSubject>
{
   public FEduQLinkClassSubjectDelete(long id) : base(x => x.LinkClassSubjectId == id)
   {
   }
}