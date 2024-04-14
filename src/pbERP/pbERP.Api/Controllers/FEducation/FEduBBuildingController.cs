using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pbERP.Api.Errors;
using pbERP.Api.Helpers;
using pbERP.Domain.DTOs.Education;
using pbERP.Domain.DTOs.FEducation;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts;
using pbERP.Infrastructure.DataMapping;
using pbERP.Infrastructure.Specifications;
using pbERP.Infrastructure.Specifications.Education;
using pbERP.Utilities.Constant;

namespace pbERP.Api.Controllers.FEducation;

public class FEduBBuildingController : BaseApiController
{
   private readonly IUnitOfWork context;

   public FEduBBuildingController(IUnitOfWork context)
   {
      this.context = context;
   }
   #region CreateEduBBuilding
   [Route(RouteConstant.CreateEduBBuilding)]
   [HttpPost]
   [ProducesResponseType(typeof(FEduBBuildingDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> CreateFEduBBuilding(FEduBBuildingDto model)
   {
      if (model == null || model.BuildingId != 0 || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));
      if (await context.EduBBuilding.IsExists(x => x.BuildingName == model.BuildingName)) return Conflict(new ApiResponse(409, MessageConstants.DuplicateError));

      model.BuildingId = await context.EduBBuilding.GetNextId("BuildingId");

      FEduBBuilding record = GenericDataMapping.DtoToEntity<FEduBBuilding, FEduBBuildingDto>(model);
      context.EduBBuilding.AddAsync(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion CreateEduBBuilding

   #region ReadEduBBuildings
   [Route(RouteConstant.ReadEduBBuildings)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduBBuildingDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduBBuildings([FromQuery] SpecificationParams specParams)
   {
      var spec = new FEduBBuildingSpecification(specParams);
      IReadOnlyList<FEduBBuilding> records = await context.EduBBuilding.ListAsyncWithSpec(spec);
      return (records.Count == 0) ? Ok(new List<FEduBBuildingDto>()) : Ok(EducationMappingProfile.BuidingEntitiesToDtos(records));
   }
   #endregion ReadEduBBuildings

   #region ReadEduBBuildingByKey
   [Route(RouteConstant.ReadEduBBuildingByKey)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduBBuildingDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduBBuilding(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new FEduBBuildingSpecification(key);
      FEduBBuilding record = await context.EduBBuilding.GetByKeyWithSpec(spec);
      return (record == null) ? NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError))
         : Ok(EducationMappingProfile.BuildingEntityToDto(record));
   }
   #endregion ReadEduBBuildingByKey

   #region UpdateEduBBuilding
   [Route(RouteConstant.UpdateEduBBuilding)]
   [HttpPut]
   [ProducesResponseType(typeof(FEduBBuildingDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> UpdateFEduBBuilding(long key, FEduBBuildingDto model)
   {
      if (key != model.BuildingId) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));
      FEduBBuilding building = await context.EduBBuilding.GetFirstOrDefaultAsync(x => x.BuildingId == model.BuildingId);
      if (building == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));
      if (building.BuildingName.Trim().ToLower() != model.BuildingName.Trim().ToLower()) if (await context.EduBBuilding.IsExists(x => x.BuildingName.Trim().ToLower() == model.BuildingName.Trim().ToLower()) == true) return Conflict(new ApiResponse(409, MessageConstants.DuplicateError));

      FEduBBuilding record = GenericDataMapping.ReplaceEntityWithDto(building, model);
      context.EduBBuilding.UpdateEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion UpdateEduBBuilding

   #region DeleteEduBBuilding
   [Route(RouteConstant.DeleteEduBBuilding)]
   [HttpDelete]
   [ProducesResponseType(typeof(FEduBBuilding), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> DeleteFEduBBuilding(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new FEduBBuildingDelete(key);
      FEduBBuilding record = await context.EduBBuilding.GetByKeyWithSpec(spec);
      if (record == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));
      if (record.FEduBClassOrHallRooms.Count != 0) return BadRequest(new ApiResponse(400, MessageConstants.IfDeleteReffereceRecord));
      context.EduBBuilding.DeleteEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.DeleteFailed)) : Ok();
   }
   #endregion DeleteEduBBuilding
}
