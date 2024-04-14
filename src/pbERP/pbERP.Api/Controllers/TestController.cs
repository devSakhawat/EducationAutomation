using Microsoft.AspNetCore.Mvc;
using pbERP.DataStructure;
using pbERP.Domain.Models;
using pbERP.Domain.Models.EAccounts;
using pbERP.Domain.Models.FEducation;

namespace pbERP.Api.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class TestController : ControllerBase
   {
      private readonly pbERPContext context;

      public TestController(pbERPContext context)
      {
         this.context = context;
      }

      [HttpGet]
      public IActionResult GetTest()
      {
         List<EAccAFinancialYear> accAFinancialYears = context.EAccAFinancialYears.ToList();

         List<FEduBBuilding> NameAsc = context.FEduBBuildings.OrderByDescending(e => e.BuildingName).ToList();
         //List<EduABuildingInfo> nameAscWithCompany  = context.EduABuildingInfos.OrderBy(e => e.BuildingNameEnglish).Include(e => e.Company).ToList();
         //List<EduABuildingInfo> NameDesc = context.EduABuildingInfos.OrderByDescending(e => e.Company.CompanyNameEnglish).ToList();
         return Ok(NameAsc);
      }
   }
}
