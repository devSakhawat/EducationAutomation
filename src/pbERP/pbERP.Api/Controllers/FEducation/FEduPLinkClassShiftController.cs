using Microsoft.AspNetCore.Mvc;
using pbERP.Api.Errors;
using pbERP.Api.Helpers;
using pbERP.Domain.DTOs.FEducation;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts;
using pbERP.Infrastructure.Specifications;
using pbERP.Infrastructure.Specifications.Education;
using pbERP.Utilities.Constant;

namespace pbERP.Api.Controllers.FEducation;

public class FEduPLinkClassShiftController : BaseApiController
{
   private readonly IUnitOfWork context;

   public FEduPLinkClassShiftController(IUnitOfWork context)
   {
      this.context = context;
   }

   #region CreateFEduPLinkClassShift
   [Route(RouteConstant.CreateFEduPLinkClassShift)]
   [HttpPost]
   [ProducesResponseType(typeof(FEduPLinkClassShiftDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> CreateFEduPLinkClassShift(List<FEduPLinkClassShift> models)
   {
      if (models.Count == 0 || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));

      int results = await context.LinkClassShift.BulkInsertLinkClassShift(models);
      return (results != 200) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion CreateFEduPLinkClassShift

   #region ReadFEduPLinkClassShift
   [Route(RouteConstant.ReadFEduPLinkClassShifts)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduPLinkClassShiftDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduPLinkClassShift([FromQuery] SpecificationParams specParams)
   {
      var spec = new FEduPLinkClassShiftSpecification(specParams);
      IReadOnlyList<FEduPLinkClassShift> records = await context.LinkClassShift.ListAsyncWithSpec(spec);
      return (records.Count <= 0) ? Ok(new List<FEduPLinkClassShift>()) : Ok(EducationMappingProfile.LinkClassShiftsToDtos(records));
   }
   #endregion ReadFEduPLinkClassShift
}
