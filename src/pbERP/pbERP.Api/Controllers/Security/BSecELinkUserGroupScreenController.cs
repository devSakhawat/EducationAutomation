using Microsoft.AspNetCore.Mvc;
using pbERP.Api.Errors;
using pbERP.Api.Helpers;
using pbERP.Domain.DTOs.BSecurity;
using pbERP.Domain.Models.BSecurity;
using pbERP.Infrastructure.Constracts;
using pbERP.Infrastructure.DataMapping;
using pbERP.Infrastructure.Specifications;
using pbERP.Infrastructure.Specifications.SecurityModule;
using pbERP.Utilities.Constant;

namespace pbERP.Api.Controllers.Security;

public class BSecELinkUserGroupScreenController : BaseApiController
{
  private readonly IUnitOfWork context;

  public BSecELinkUserGroupScreenController(IUnitOfWork context)
  {
    this.context = context;
  }

  #region CreateSecELinkUserGroupScreen
  [Route(RouteConstant.CreateSecELinkUserGroupScreen)]
  [HttpPost]
  [ProducesResponseType(typeof(BSecELinkUserGroupScreenDto), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
  public async Task<IActionResult> CreateSecELinkUserGroupScreen(BSecELinkUserGroupScreenDto model)
  {
    if (model == null || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));
    //if (await context.LinkUserGroupScreen.IsDuplicate(x => x.UserGroupId == model.UserGroupId) == true)
    //   return Conflict(new ApiResponse(409, "UserGroupId " + model.UserGroupId + " " + MessageConstants.DuplicateError));
    //if (await context.LinkUserGroupScreen.IsDuplicate(x => x.ScreenId == model.ScreenId) == true)
    //   return Conflict(new ApiResponse(409, "ScreenId" + model.ScreenId + " " + MessageConstants.DuplicateError));

    model.LinkUserGroupScreenId = await context.LinkUserGroupScreen.GetNextId("LinkUserGroupScreenId");
    BSecELinkUserGroupScreen record = GenericDataMapping.DtoToEntity<BSecELinkUserGroupScreen, BSecELinkUserGroupScreenDto>(model);
    if (record == null) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordInsert));

    context.LinkUserGroupScreen.AddAsync(record);
    var saveChanges = await context.SaveChangesAsync();
    if (saveChanges <= 0) return BadRequest(new ApiResponse(400, MessageConstants.SaveFailed));
    return Ok();
  }
  #endregion CreateSecELinkUserGroupScreen

  #region BatchInsertSecELinkUserGroupScreen
  [Route(RouteConstant.BatchInsertSecELinkUserGroupScreen)]
  [HttpPost]
  [ProducesResponseType(typeof(BSecELinkUserGroupScreenDto), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
  public async Task<IActionResult> BatchInsertSecELinkUserGroupScreen(List<BSecELinkUserGroupScreenDto> models)
  {
    if (models.Count() == 0 || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));
    //if (await context.LinkUserGroupScreen.IsDuplicate(x => x.UserGroupId == model.UserGroupId) == true)
    //   return Conflict(new ApiResponse(409, "UserGroupId " + model.UserGroupId + " " + MessageConstants.DuplicateError));
    //if (await context.LinkUserGroupScreen.IsDuplicate(x => x.ScreenId == model.ScreenId) == true)
    //   return Conflict(new ApiResponse(409, "ScreenId" + model.ScreenId + " " + MessageConstants.DuplicateError));

    long linkUserGroupScreenId = await context.LinkUserGroupScreen.GetNextId("LinkUserGroupScreenId");

    long patchIdValue = linkUserGroupScreenId;
    var records = models.Select(x => new BSecELinkUserGroupScreen { LinkUserGroupScreenId = patchIdValue++, ScreenId = x.ScreenId, UserGroupId = (long)x.UserGroupId }).ToList();
    int result = await context.LinkUserGroupScreen.InsertLinkUserGroupScreen(records);

    return (result <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
    //if (result <= 0) return BadRequest(new ApiResponse(400, MessageConstants.SaveFailed));
    //return Ok();
  }
  #endregion BatchInsertSecELinkUserGroupScreen

  #region ReadSecELinkUserGroupScreens
  [Route(RouteConstant.ReadSecELinkUserGroupScreens)]
  [HttpGet]
  [ProducesResponseType(typeof(BSecELinkUserGroupScreenDto), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
  public async Task<IActionResult> ReadReadSecELinkUserGroupScreensSecBUser([FromQuery] SpecificationParams specParams)
  {
    //var countSpec = new BSecELinkUserGroupScreenSpecification(specParams);
    //var totalItems = await context.LinkUserGroupScreen.CountAsync(countSpec);

    //var spec = new BSecELinkUserGroupScreenSpecification(specParams);
    //IReadOnlyList<BSecELinkUserGroupScreen> userGroupScreens = await context.LinkUserGroupScreen.ListAsyncWithSpec(spec);

    IReadOnlyList<BSecELinkUserGroupScreen> userGroupScreens = await context.LinkUserGroupScreen.ListAsync(x => x.LinkUserGroupScreenId, includeProperties: "Screen,UserGroup");
    if (userGroupScreens.Count() == 0) return NotFound(new ApiResponse(404, MessageConstants.NoRecordError));

    //var records = GenericDataMapping.EntitiesToDtos<BSecELinkUserGroupScreen, BSecELinkUserGroupScreenDto>(records);
    //return Ok(new Pagination<BSecELinkUserGroupScreenDto>(specParams.PageIndex, specParams.PageSize, totalItems, users));

    //IReadOnlyList<BSecELinkUserGroupScreen> userGroupScreens = await context.LinkUserGroupScreen.ListAsyncDesc(x => x.LinkUserGroupScreenId);
    //if (users.Count() == 0) return NotFound(new ApiResponse(404, MessageConstants.NoRecordError));

    IReadOnlyList<BSecELinkUserGroupScreenDto> records = SecurityMappingProfile.UserGroupScreenEntitiesToDtos(userGroupScreens);
    return Ok(records);
  }
  #endregion ReadSecELinkUserGroupScreens

  #region ReadSecELinkUserGroupScreenByKey
  [Route(RouteConstant.ReadSecELinkUserGroupScreenByKey)]
  [HttpGet]
  [ProducesResponseType(typeof(BSecELinkUserGroupScreenDto), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
  public async Task<IActionResult> BSecELinkUserGroupScreenByKey(long key)
  {
    if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
    var spec = new BSecELinkUserGroupScreenSpecification(key);

    BSecELinkUserGroupScreen userGroupScreen = await context.LinkUserGroupScreen.GetByKeyWithSpec(spec);
    if (userGroupScreen == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));
    BSecELinkUserGroupScreenDto record = SecurityMappingProfile.UserGroupScreenEntityToDto(userGroupScreen);
    return Ok(record);
  }
  #endregion ReadSecELinkUserGroupScreenByKey

  #region UpdateSecELinkUserGroupScreen
  [Route(RouteConstant.UpdateSecELinkUserGroupScreen)]
  [HttpPut]
  [ProducesResponseType(typeof(BSecELinkUserGroupScreenDto), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
  public async Task<IActionResult> UpdateSecELinkUserGroupScreen(long key, BSecELinkUserGroupScreenDto model)
  {
    if (key == 0 || key != model.LinkUserGroupScreenId) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));
    var spec = new BSecELinkUserGroupScreenSpecification(key);
    BSecELinkUserGroupScreen userGroupScreen = await context.LinkUserGroupScreen.GetByKeyWithSpec(spec);
    if (userGroupScreen == null) return BadRequest(new ApiResponse(400, MessageConstants.NoMatchFoundError));
    BSecELinkUserGroupScreen record = GenericDataMapping.ReplaceEntityWithDto(userGroupScreen, model);

    context.LinkUserGroupScreen.UpdateEntity(record);
    var saveChanges = await context.SaveChangesAsync();
    if (saveChanges <= 0) return BadRequest(new ApiResponse(400, MessageConstants.SaveFailed));
    return Ok();

  }
  #endregion UpdateSecBUser

  #region DeleteSecELinkUserGroupScreen
  [Route(RouteConstant.DeleteSecELinkUserGroupScreen)]
  [HttpDelete]
  [ProducesResponseType(typeof(BSecELinkUserGroupScreenDto), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
  public async Task<IActionResult> DeleteSecELinkUserGroupScreen(long key)
  {
    if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordDeleteError));
    var spec = new BSecELinkUserGroupScreenSpecification(key);
    // Check AddInclude for foreign key refference;
    BSecELinkUserGroupScreen record = await context.LinkUserGroupScreen.GetByKeyWithSpec(spec);
    if (record == null) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordDeleteError));

    context.LinkUserGroupScreen.DeleteEntity(record);
    var saveChanges = await context.SaveChangesAsync();
    if (saveChanges <= 0) return BadRequest(new ApiResponse(400, MessageConstants.SaveFailed));

    return Ok();
  }
  #endregion DeleteSecELinkUserGroupScreen
}