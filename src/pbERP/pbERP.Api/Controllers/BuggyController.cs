using Microsoft.AspNetCore.Mvc;
using pbERP.Api.Errors;
using pbERP.DataStructure;
using pbERP.Domain.Models.FEducation;

namespace pbERP.Api.Controllers
{
   public class BuggyController : BaseApiController
   {
      private readonly pbERPContext context;

      public BuggyController(pbERPContext context)
      {
         this.context = context;
      }

      [HttpGet("notfound")]
      public IActionResult GetNotFoundResult()
      {
         //var result = context.AccAFinancialYears.Find(100);

         //if (result == null) return NotFound(new ApiResponse(404));
         return NotFound(new ApiResponse(404));
      }

      [HttpGet("badrequest")]
      public IActionResult GetBadRequest()
      {
         return BadRequest(new ApiResponse(400));
      }

      [HttpGet("badrequest/{id}")]
      public IActionResult GetNotFoundByIdRequest(int id)
      {
         var result = context.EAccAFinancialYears.Find(id);

         if (result == null) return NotFound(new ApiResponse(404));
         return Ok();
      }

      [HttpGet("server-error")]
      public IActionResult GetServerError()
      {
         var thing = context.FEduBBuildings.Find(42);

         var thingToReturn = thing.ToString();

         return Ok(thingToReturn);
      }

      [HttpGet]
      public IQueryable<FEduBBuilding> GetByIQueryable()
      {
         var eduBuildingInfos = context.FEduBBuildings.AsQueryable().ToList();
         return (IQueryable<FEduBBuilding>)eduBuildingInfos;
      }
   }
}