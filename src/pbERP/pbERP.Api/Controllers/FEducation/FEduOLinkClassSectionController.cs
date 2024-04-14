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

public class FEduOLinkClassSectionController : BaseApiController
{
   private readonly IUnitOfWork context;

   public FEduOLinkClassSectionController(IUnitOfWork context)
   {
      this.context = context;
   }

   #region CreateFEduOLinkClassSection
   [Route(RouteConstant.CreateFEduOLinkClassSection)]
   [HttpPost]
   [ProducesResponseType(typeof(FEduOLinkClassSectionDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> CreateFEduTransportArea(List<FEduOLinkClassSectionDto> models)
   {
      if (models.Count() == 0 || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));

      int results = await context.LinkClassSection.BulkInsertLinkClassSection(models);
      return (results <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion CreateFEduOLinkClassSection

   #region ReadFEduOLinkClassSections
   [Route(RouteConstant.ReadFEduOLinkClassSectiones)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduOLinkClassSectionDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduOLinkClassSections([FromQuery] SpecificationParams specParams)
   {
      var spec = new FEduOLinkClassSectionSpecification(specParams);
      IReadOnlyList<FEduOLinkClassSection> records = await context.LinkClassSection.ListAsyncWithSpec(spec);
      return (records.Count <= 0) ? Ok(new List<FEduFTransportChargeDto>()) : Ok(EducationMappingProfile.LinkClassSectionesToDtos(records));
   }
   #endregion ReadFEduOLinkClassSections
}
