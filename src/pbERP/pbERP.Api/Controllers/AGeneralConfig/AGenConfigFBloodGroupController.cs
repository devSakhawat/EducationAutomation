using Microsoft.AspNetCore.Mvc;
using pbERP.Api.Errors;
using pbERP.Domain.DTOs.AGeneralConfig;
using pbERP.Domain.Models.AGeneralConfig;
using pbERP.Infrastructure.Constracts;
using pbERP.Infrastructure.DataMapping;
using pbERP.Infrastructure.Specifications;
using pbERP.Infrastructure.Specifications.GeneralConfig;
using pbERP.Utilities.Constant;
using System.Reflection;

namespace pbERP.Api.Controllers.AGenaralConfig;

public class AGenConfigFBloodGroupController : BaseApiController
{
   private readonly IUnitOfWork context;

   public AGenConfigFBloodGroupController(IUnitOfWork context)
   {
      this.context = context;
   }

   #region CreateAGenConfigFBloodGroup
   [Route(RouteConstant.CreateAGenConfigFBloodGroup)]
   [HttpPost]
   [ProducesResponseType(typeof(AGenConfigFBloodGroupDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> CreateAGenConfigFBloodGroup(AGenConfigFBloodGroupDto model)
   {
      if (model == null || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));
      if (await context.BloodGroup.IsDuplicate(x => x.BloodGroupName == model.BloodGroupName) == true)
         return Conflict(new ApiResponse(409, model.BloodGroupId + " " + MessageConstants.DuplicateError));

      model.BloodGroupId = await context.BloodGroup.GetNextId("BloodGroupId");
      AGenConfigFBloodGroup record = GenericDataMapping.DtoToEntity<AGenConfigFBloodGroup, AGenConfigFBloodGroupDto>(model);
      if (record == null) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordInsert));

      context.BloodGroup.AddAsync(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion CreateAGenConfigFBloodGroup

   #region ReadAGenConfigFBloodGroups
   [Route(RouteConstant.ReadAGenConfigFBloodGroups)]
   [HttpGet]
   [ProducesResponseType(typeof(AGenConfigGReligionDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> ReadAGenConfigFBloodGroups([FromQuery] SpecificationParams specParams)
   {
      var spec = new AGenConfigFBloodGroupSpecification(specParams);
      IReadOnlyList<AGenConfigFBloodGroup> bloodGroups = await context.BloodGroup.ListAsyncWithSpec(spec);
      return (bloodGroups.Count == 0) ? BadRequest(new ApiResponse(404, MessageConstants.NoRecordError)) : Ok(GenericDataMapping.EntitiesToDtos<AGenConfigFBloodGroup, AGenConfigFBloodGroupDto>(bloodGroups));
   }
   #endregion ReadAGenConfigFBloodGroups

   #region ReadAGenConfigGReligionByKey
   [Route(RouteConstant.ReadAGenConfigFBloodGroupByKey)]
   [HttpGet]
   [ProducesResponseType(typeof(AGenConfigGReligionDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> ReadAGenConfigFBloodGroupByKey(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new AGenConfigFBloodGroupSpecification(key);

      AGenConfigFBloodGroup bloodGroup = await context.BloodGroup.GetByKeyWithSpec(spec);
      return (bloodGroup == null) ? NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError))
         : Ok(GenericDataMapping.EntityToDto<AGenConfigFBloodGroup, AGenConfigFBloodGroupDto>(bloodGroup));
   }
   #endregion ReadAGenConfigFBloodGroupByKey

   #region UpdateAGenConfigFBloodGroup
   [Route(RouteConstant.UpdateAGenConfigFBloodGroup)]
   [HttpPut]
   [ProducesResponseType(typeof(AGenConfigGReligionDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> UpdateAGenConfigFBloodGroup(long key, AGenConfigFBloodGroupDto model)
   {
      if (key == 0 || key != model.BloodGroupId) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));
      var spec = new AGenConfigFBloodGroupSpecification(key);
      AGenConfigFBloodGroup bloodGroup = await context.BloodGroup.GetByKeyWithSpec(spec);
      if (bloodGroup == null) return BadRequest(new ApiResponse(400, MessageConstants.NoMatchFoundError));
      AGenConfigFBloodGroup record = GenericDataMapping.ReplaceEntityWithDto(bloodGroup, model);

      context.BloodGroup.UpdateEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion UpdateAGenConfigFBloodGroup

   #region DeleteAGenConfigFBloodGroup
   [Route(RouteConstant.DeleteAGenConfigFBloodGroup)]
   [HttpDelete]
   [ProducesResponseType(typeof(AGenConfigGReligionDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> DeleteAGenConfigFBloodGroup(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordDeleteError));
      var spec = new AGenConfigFBloodGroupDelete(key);
      AGenConfigFBloodGroup bloodGroup = await context.BloodGroup.GetByKeyWithSpec(spec);

      if (bloodGroup.DHrJEmployees.Count != 0 && bloodGroup.FEduAStudents.Count != 0)
         return BadRequest(new ApiResponse(400, MessageConstants.IfDeleteReffereceRecord));
      if (bloodGroup == null) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordDeleteError));

      context.BloodGroup.DeleteEntity( bloodGroup );
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion DeleteAGenConfigFBloodGroup
}