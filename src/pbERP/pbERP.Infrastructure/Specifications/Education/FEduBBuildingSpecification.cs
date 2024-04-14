using pbERP.Domain.Models.AGeneralConfig;
using pbERP.Domain.Models.FEducation;

namespace pbERP.Infrastructure.Specifications.Education
{
   public class FEduBBuildingSpecification : BaseSpecification<FEduBBuilding>
   {
      public FEduBBuildingSpecification(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.BuildingName.ToLower().Contains(specParams.Search))
      {
         AddInclude(x => x.Company);
         AddOrderByDescending(s => s.BuildingId);
         ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

         if (!string.IsNullOrEmpty(specParams.Sort))
         {
            switch (specParams.Sort)
            {
               case "nameAsc": AddOrderBy(s => s.BuildingName); break;
               case "nameDesc": AddOrderByDescending(s => s.BuildingName); break;
               default: AddOrderBy(s => s.BuildingId); break;
            }
         }
      }
      public FEduBBuildingSpecification(long id) : base(e => e.BuildingId == id)
      {
         AddInclude(x => x.Company);
      }
   }

   public class FEduBBuildingCount : BaseSpecification<FEduBBuilding>
   {
      public FEduBBuildingCount(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.BuildingName.ToLower().Contains(specParams.Search))
      {
      }
   }

   public class FEduBBuildingDelete : BaseSpecification<FEduBBuilding>
   {
      public FEduBBuildingDelete(long id) : base(x => x.BuildingId == id)
      {
         AddInclude(x => x.FEduBClassOrHallRooms);
      }
   }
}