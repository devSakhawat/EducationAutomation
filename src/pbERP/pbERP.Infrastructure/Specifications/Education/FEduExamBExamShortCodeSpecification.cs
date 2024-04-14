using pbERP.Domain.Models.FEducation;

namespace pbERP.Infrastructure.Specifications.Education
{
   public class FEduExamBExamShortCodeSpecification : BaseSpecification<FEduExamBExamShortCode>
   {
      public FEduExamBExamShortCodeSpecification(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.ExamShortCode.ToLower().Contains(specParams.Search))
      {
         AddOrderByDescending(s => s.ExamShortCodeId);
         ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

         if (!string.IsNullOrEmpty(specParams.Sort))
         {
            switch (specParams.Sort)
            {
               case "nameAsc": AddOrderBy(s => s.ExamShortCode); break;
               case "nameDesc": AddOrderByDescending(s => s.ExamShortCode); break;
               default: AddOrderBy(s => s.ExamShortCodeId); break;
            }
         }
      }
      public FEduExamBExamShortCodeSpecification(long id) : base(e => e.ExamShortCodeId == id)
      {
      }
   }

   public class FEduExamBExamShortCodeCount : BaseSpecification<FEduExamBExamShortCode>
   {
      public FEduExamBExamShortCodeCount(SpecificationParams specParams) : base(x => string.IsNullOrEmpty(specParams.Search) || x.ExamShortCode.ToLower().Contains(specParams.Search))
      {
      }
   }

   public class FEduExamBExamShortCodeDelete : BaseSpecification<FEduExamBExamShortCode>
   {
      public FEduExamBExamShortCodeDelete(long id) : base(x => x.ExamShortCodeId == id)
      {
      }
   }
}