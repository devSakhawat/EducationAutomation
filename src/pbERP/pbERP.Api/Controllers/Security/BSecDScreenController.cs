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

public class BSecDScreenController : BaseApiController
{
   private readonly IUnitOfWork context;

   public BSecDScreenController(IUnitOfWork context)
   {
      this.context = context;
   }

   #region CreateBSecDScreen
   [Route(RouteConstant.CreateBSecDScreen)]
   [HttpPost]
   [ProducesResponseType(typeof(BSecDScreenDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> CreateBSecDScreen(BSecDScreenDto model)
   {
      if (model == null || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));
      if (await context.Screen.IsDuplicate(x => x.ScreenName == model.ScreenName) == true)
         return Conflict(new ApiResponse(409, model.ScreenName + " " + MessageConstants.DuplicateError));

      model.ScreenId = await context.Screen.GetNextId("ScreenId");
      BSecDScreen record = GenericDataMapping.DtoToEntity<BSecDScreen, BSecDScreenDto>(model);
      if (record == null) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordInsert));

      context.Screen.AddAsync(record);
      var saveChanges = await context.SaveChangesAsync();
      if (saveChanges <= 0) return BadRequest(new ApiResponse(400, MessageConstants.SaveFailed));
      return Ok();
   }
   #endregion CreateBSecDScreen

   #region ReadBSecDScreens
   [Route(RouteConstant.ReadBSecDScreens)]
   [HttpGet]
   [ProducesResponseType(typeof(BSecDScreenDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> ReadBSecDScreens([FromQuery] SpecificationParams specParams)
   {
      //var countSpec = new BSecDScreenSpecification(specParams);
      //var totalItems = await context.Screen.CountAsync(countSpec);
      //var spec = new BSecDScreenSpecification(specParams);
      //var records = GenericDataMapping.EntitiesToDtos<BSecDScreen, BSecDScreenDto>(screens);
      //return Ok(new Pagination<BSecELinkUserGroupScreenDto>(specParams.PageIndex, specParams.PageSize, totalItems, users));

      IReadOnlyList<BSecDScreen> screens = await context.Screen.ListAsync(x => x.ScreenId, includeProperties: "Module");
      if (screens.Count() == 0) return NotFound(new ApiResponse(404, MessageConstants.NoRecordError));
      IReadOnlyList<BSecDScreenDto> records = SecurityMappingProfile.ScreenEntitiesToDtos(screens);

      return Ok(records);
   }
   #endregion ReadSecELinkUserGroupScreens

   #region ReadBSecDScreenByKey
   [Route(RouteConstant.ReadBSecDScreenByKey)]
   [HttpGet]
   [ProducesResponseType(typeof(BSecDScreenDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> BSecDScreenByKey(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new BSecDScreenSpecification(key);

      BSecDScreen screen = await context.Screen.GetByKeyWithSpec(spec);
      if (screen == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));
      BSecDScreenDto record = SecurityMappingProfile.ScreenEntityToDto(screen);
      return Ok(record);
   }
   #endregion ReadSecELinkUserGroupScreenByKey

   #region UpdateBSecDScreen
   [Route(RouteConstant.UpdateBSecDScreen)]
   [HttpPut]
   [ProducesResponseType(typeof(BSecDScreenDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> UpdateBSecDScreen(long key, BSecDScreenDto model)
   {
      if (key == 0 || key != model.ScreenId) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));
      var spec = new BSecDScreenSpecification(key);
      BSecDScreen screen = await context.Screen.GetByKeyWithSpec(spec);
      if (screen == null) return BadRequest(new ApiResponse(400, MessageConstants.NoMatchFoundError));
      BSecDScreen record = GenericDataMapping.ReplaceEntityWithDto(screen, model);

      context.Screen.UpdateEntity(record);
      var saveChanges = await context.SaveChangesAsync();
      if (saveChanges <= 0) return BadRequest(new ApiResponse(400, MessageConstants.SaveFailed));
      return Ok();

   }
   #endregion UpdateBSecDScreen

   #region DeleteBSecDScreen
   [Route(RouteConstant.DeleteBSecDScreen)]
   [HttpDelete]
   [ProducesResponseType(typeof(BSecDScreenDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> DeleteBSecDScreen(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordDeleteError));
      var spec = new BSecDScreenDelete(key);
      BSecDScreen record = await context.Screen.GetByKeyWithSpec(spec);

      if ( record.BSecELinkUserGroupScreens.Count != 0 && record.BSecFScreenCommands.Count != 0 )
         return BadRequest(new ApiResponse(400, MessageConstants.IfDeleteReffereceRecord));
      if (record == null) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordDeleteError));

      context.Screen.DeleteEntity(record);
      var saveChanges = await context.SaveChangesAsync();
      if (saveChanges <= 0) return BadRequest(new ApiResponse(400, MessageConstants.SaveFailed));

      return Ok();
   }
   #endregion DeleteBSecDScreen
}