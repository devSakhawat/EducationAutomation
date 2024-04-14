using Microsoft.AspNetCore.Mvc;
using pbERP.Api.Errors;
using pbERP.Api.Helpers;
using pbERP.Domain.DTOs.FEducation;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts;
using pbERP.Infrastructure.DataMapping;
using pbERP.Infrastructure.Specifications;
using pbERP.Infrastructure.Specifications.Education;
using pbERP.Utilities.Constant;

namespace pbERP.Api.Controllers.FEducation;

public class FEduKClassGroupController : BaseApiController
{
   private readonly IUnitOfWork context;

   public FEduKClassGroupController(IUnitOfWork context)
   {
      this.context = context;
   }

   #region CreateFEduKClassGroup
   [Route(RouteConstant.CreateFEduKClassGroup)]
   [HttpPost]
   [ProducesResponseType(typeof(FEduKClassGroupDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> CreateFEduKClassGroup(FEduKClassGroupDto model)
   {
      if (model == null || model.ClassGroupId != 0 || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));
      if (await context.ClassGroup.IsExists(x => x.ClassGroupName == model.ClassGroupName)) return Conflict(new ApiResponse(409, MessageConstants.DuplicateError));

      model.ClassGroupId = await context.ClassGroup.GetNextId("ClassGroupId");
      FEduKClassGroup record = GenericDataMapping.DtoToEntity<FEduKClassGroup, FEduKClassGroupDto>(model);
      context.ClassGroup.AddAsync(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion CreateFEduKClassGroup

   #region ReadFEduKClassGroups
   [Route(RouteConstant.ReadFEduKClassGroups)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduKClassGroupDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduKClassGroups([FromQuery] SpecificationParams specParams)
   {
      var spec = new FEduKClassGroupSpecification(specParams);
      IReadOnlyList<FEduKClassGroup> records = await context.ClassGroup.ListAsyncWithSpec(spec);
      return (records.Count() <= 0) ? Ok(new List<FEduKClassGroupDto>()) : Ok(GenericDataMapping.EntitiesToDtos<FEduKClassGroup, FEduKClassGroupDto>(records));
   }
   #endregion ReadFEduKClassGroups

   #region ReadFEduKClassGroupByKey
   [Route(RouteConstant.ReadFEduKClassGroupByKey)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduKClassGroupDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduKClassGroup(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new FEduKClassGroupSpecification(key);
      FEduKClassGroup record = await context.ClassGroup.GetByKeyWithSpec(spec);
      return (record == null) ? NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError)) : Ok(GenericDataMapping.EntityToDto< FEduKClassGroup, FEduKClassGroupDto>(record));
   }
   #endregion ReadFEduKClassGroupByKey

   #region UpdateFEduKClassGroup
   [Route(RouteConstant.UpdateFEduKClassGroup)]
   [HttpPut]
   [ProducesResponseType(typeof(FEduKClassGroupDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> UpdateFEduKClassGroup(long key, FEduKClassGroupDto model)
   {
      if (key != model.ClassGroupId) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));
      FEduKClassGroup classSection = await context.ClassGroup.GetFirstOrDefaultAsync(x => x.ClassGroupId == model.ClassGroupId);
      if (classSection == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));

      FEduKClassGroup record = GenericDataMapping.ReplaceEntityWithDto(classSection, model);
      context.ClassGroup.UpdateEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion UpdateFEduKClassGroup

   #region DeleteFEduKClassGroup
   [Route(RouteConstant.DeleteFEduKClassGroup)]
   [HttpDelete]
   [ProducesResponseType(typeof(FEduKClassGroupDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> DeleteFEduKClassGroup(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new FEduKClassGroupDelete(key);
      FEduKClassGroup record = await context.ClassGroup.GetByKeyWithSpec(spec);
      if (record == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));

      context.ClassGroup.DeleteEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.DeleteFailed)) : Ok();
   }
   #endregion DeleteFEduKClassGroup
}
