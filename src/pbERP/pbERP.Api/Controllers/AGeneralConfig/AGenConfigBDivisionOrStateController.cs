using Microsoft.AspNetCore.Mvc;
using pbERP.Api.Errors;
using pbERP.Api.Helpers;
using pbERP.Domain.DTOs.AGeneralConfig;
using pbERP.Domain.Models.AGeneralConfig;
using pbERP.Infrastructure.Constracts;
using pbERP.Infrastructure.DataMapping;
using pbERP.Infrastructure.Specifications;
using pbERP.Infrastructure.Specifications.GeneralConfig;
using pbERP.Utilities.Constant;

namespace pbERP.Api.Controllers.AGenaralConfig;

public class AGenConfigBDivisionOrStateController : BaseApiController
{
   private readonly IUnitOfWork context;

   public AGenConfigBDivisionOrStateController(IUnitOfWork context)
   {
      this.context = context;
   }

   #region CreateAGenConfigBDivisionOrState
   [Route(RouteConstant.CreateAGenConfigBDivisionOrState)]
   [HttpPost]
   [ProducesResponseType(typeof(AGenConfigBDivisionOrStateDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> CreateAGenConfigBDivisionOrState(AGenConfigBDivisionOrStateDto model)
   {
      if (model == null || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));
      if (await context.Division.IsDuplicate(x => x.DivisionName == model.DivisionName) == true)
         return Conflict(new ApiResponse(409, model.DivisionName + " " + MessageConstants.DuplicateError));

      model.DivisionId = await context.Division.GetNextId("DivisionId");
      AGenConfigBDivisionOrState record = GenericDataMapping.DtoToEntity<AGenConfigBDivisionOrState, AGenConfigBDivisionOrStateDto>(model);
      if (record == null) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordInsert));

      context.Division.AddAsync(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion CreateAGenConfigBDivisionOrState

   #region ReadAGenConfigBDivisionOrStates
   [Route(RouteConstant.ReadAGenConfigBDivisionOrStates)]
   [HttpGet]
   [ProducesResponseType(typeof(AGenConfigBDivisionOrStateDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> ReadAGenConfigACountrys([FromQuery] SpecificationParams specParams)
   {
      var spec = new AGenConfigBDivisionOrStateSpecification(specParams);
      IReadOnlyList<AGenConfigBDivisionOrState> divisionOrStates = await context.Division.ListAsyncWithSpec(spec);
      return (divisionOrStates.Count == 0) ? BadRequest(new ApiResponse(404, MessageConstants.NoRecordError)) 
         : Ok(GeneralConfigMappingProfile.DivisionEntitiesToDtos(divisionOrStates));
   }
   #endregion ReadAGenConfigIModules

   #region ReadAGenConfigBDivisionOrStateByKey
   [Route(RouteConstant.ReadAGenConfigBDivisionOrStateByKey)]
   [HttpGet]
   [ProducesResponseType(typeof(AGenConfigBDivisionOrStateDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> ReadAGenConfigBDivisionOrStateByKey(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new AGenConfigBDivisionOrStateSpecification(key);

      AGenConfigBDivisionOrState divisionOrState = await context.Division.GetByKeyWithSpec(spec);
      return (divisionOrState == null) ? NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError))
         : Ok(GeneralConfigMappingProfile.DivisionEntityToDto(divisionOrState));
   }
   #endregion ReadAGenConfigEGenderByKey

   #region UpdateAGenConfigBDivisionOrState
   [Route(RouteConstant.UpdateAGenConfigBDivisionOrState)]
   [HttpPut]
   [ProducesResponseType(typeof(AGenConfigBDivisionOrStateDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> UpdateAGenConfigBDivisionOrState(long key, AGenConfigBDivisionOrStateDto model)
   {
      if (key == 0 || key != model.DivisionId) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));
      var spec = new AGenConfigBDivisionOrStateSpecification(key);
      AGenConfigBDivisionOrState divisionOrState = await context.Division.GetByKeyWithSpec(spec);
      if (divisionOrState == null) return BadRequest(new ApiResponse(400, MessageConstants.NoMatchFoundError));
      AGenConfigBDivisionOrState record = GenericDataMapping.ReplaceEntityWithDto(divisionOrState, model);

      context.Division.UpdateEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion UpdateAGenConfigEGender

   #region DeleteAGenConfigBDivisionOrState
   [Route(RouteConstant.DeleteAGenConfigBDivisionOrState)]
   [HttpDelete]
   [ProducesResponseType(typeof(AGenConfigBDivisionOrStateDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> DeleteAGenConfigBDivisionOrState(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordDeleteError));
      var spec = new AGenConfigBDivisionOrStateDelete(key);
      AGenConfigBDivisionOrState record = await context.Division.GetByKeyWithSpec(spec);

      if (record.AGenConfigCDistrictOrCities.Count != 0 && record.AGenConfigCDistrictOrCities.Count != 0)
         return BadRequest(new ApiResponse(400, MessageConstants.IfDeleteReffereceRecord));
      if (record == null) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordDeleteError));

      context.Division.DeleteEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion DeleteAGenConfigBDivisionOrState
}