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

public class FEduNLinkClassGroupController : BaseApiController
{
  private readonly IUnitOfWork context;

  public FEduNLinkClassGroupController(IUnitOfWork context)
  {
    this.context = context;
  }

  #region CreateFEduNLinkClassGroup
  [Route(RouteConstant.CreateFEduNLinkClassGroup)]
  [HttpPost]
  [ProducesResponseType(typeof(FEduNLinkClassGroupDto), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
  public async Task<IActionResult> CreateFEduTransportArea(List<FEduNLinkClassGroupDto> models)
  {
    if (models.Count() == 0 || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));

    int results = await context.LinkClassGroup.BulkInsertLinkClassGroup(models);
    return (results != 200) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
  }
  #endregion CreateFEduNLinkClassGroup

  #region ReadFEduNLinkClassGroups
  [Route(RouteConstant.ReadFEduNLinkClassGroups)]
  [HttpGet]
  [ProducesResponseType(typeof(FEduNLinkClassGroupDto), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> ReadFEduNLinkClassGroups([FromQuery] SpecificationParams specParams)
  {
    var spec = new FEduNLinkClassGroupSpecification(specParams);
    IReadOnlyList<FEduNLinkClassGroup> records = await context.LinkClassGroup.ListAsyncWithSpec(spec);
    return (records.Count <= 0) ? Ok(new List<FEduFTransportChargeDto>()) : Ok(EducationMappingProfile.LinkClassGroupesToDtos(records));
  }
  #endregion ReadFEduNLinkClassGroups
}
