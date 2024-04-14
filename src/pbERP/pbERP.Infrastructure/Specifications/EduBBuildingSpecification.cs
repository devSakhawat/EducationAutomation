using pbERP.Domain.Models.FEducation;

namespace pbERP.Infrastructure.Specifications
{
   public class EduBBuildingSpecification : BaseSpecification<FEduBBuilding>
   {
      public EduBBuildingSpecification(SpecificationParams specParams) : base(x =>
          (string.IsNullOrEmpty(specParams.Search) || x.BuildingName.ToLower().Contains(specParams.Search)) &&
          (!specParams.Id.HasValue || x.CompanyId == specParams.Id))
      {
         AddInclude(e => e.Company);
         //AddInclude(x => x.EduBClassOrHallRoomInfos);
         AddOrderBy(e => e.BuildingName);
         ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

         if (!string.IsNullOrEmpty(specParams.Sort))
         {
            switch (specParams.Sort)
            {
               case "nameAsc":
                  AddOrderBy(e => e.Company.CompanyName); break;

               case "nameDesc":
                  AddOrderByDescending(e => e.Company.CompanyName); break;

               default:
                  AddOrderBy(e => e.BuildingName); break;
            }
         }
      }
      public EduBBuildingSpecification(int id) : base(e => e.BuildingId == id)
      {
         AddInclude(e => e.Company);
         //AddInclude(x => x.EduBClassOrHallRoomInfos);
      }
   }

   public class EduABuildingsWithFiltersForCountSpecification : BaseSpecification<FEduBBuilding>
   {
      public EduABuildingsWithFiltersForCountSpecification(SpecificationParams specParams) : base(x =>
          (string.IsNullOrEmpty(specParams.Search) || x.BuildingName.ToLower().Contains(specParams.Search)) &&
          (!specParams.Id.HasValue || x.CompanyId == specParams.Id))
      {
      }
   }
}