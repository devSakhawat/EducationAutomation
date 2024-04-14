using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.FileProviders;
using pbERP.Api.Errors;
using pbERP.Api.Helpers;
using pbERP.Domain.DTOs.AGeneralConfig;
using pbERP.Domain.Models.AGeneralConfig;
using pbERP.Infrastructure.Constracts;
using pbERP.Infrastructure.DataMapping;
using pbERP.Infrastructure.Specifications;
using pbERP.Infrastructure.Specifications.GeneralConfig;
using pbERP.Utilities.Constant;

namespace pbERP.Api.Controllers.AGeneralConfig;

public class AGenConfigCDistrictOrCityController : BaseApiController
{
   private readonly IUnitOfWork context;

   public AGenConfigCDistrictOrCityController(IUnitOfWork context)
   {
      this.context = context;
   }

   #region CreateAGenConfigCDistrictOrCity
   [Route(RouteConstant.CreateAGenConfigCDistrictOrCity)]
   [HttpPost]
   [ProducesResponseType(typeof(AGenConfigCDistrictOrCityDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> CreateAGenConfigCDistrictOrCity(AGenConfigCDistrictOrCityDto model)
   {
      if (model == null || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));
      if (await context.District.IsExists(x => x.DistrictName == model.DistrictName) == true)
         return Conflict(new ApiResponse(409, model.DistrictName + " " + MessageConstants.DuplicateError));

      model.DistrictId = await context.District.GetNextId("DistrictId");
      AGenConfigCDistrictOrCity record = GenericDataMapping.DtoToEntity<AGenConfigCDistrictOrCity, AGenConfigCDistrictOrCityDto>(model);
      if (record == null) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordInsert));

      context.District.AddAsync(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion

   #region ReadAGenConfigCDistrictOrCities
   [Route(RouteConstant.ReadAGenConfigCDistrictOrCities)]
   [HttpGet]
   [ProducesResponseType(typeof(AGenConfigCDistrictOrCityDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadAGenConfigCDistrictOrCities([FromQuery] SpecificationParams specParams)
   {
      var spec = new AGenConfigCDistrictOrCitySpecification(specParams);
      IReadOnlyList<AGenConfigCDistrictOrCity> districts = await context.District.ListAsyncWithSpec(spec);
      return (districts.Count == 0) ? BadRequest(new ApiResponse(404, MessageConstants.NoRecordError)) 
         : Ok(GeneralConfigMappingProfile.DistrictEntitiesToDtos(districts));
   }
   #endregion ReadAGenConfigCDistrictOrCities

   #region ReadAGenConfigCDistrictOrCity
   [Route(RouteConstant.ReadAGenConfigCDistrictOrCity)]
   [HttpGet]
   [ProducesResponseType(typeof(AGenConfigCDistrictOrCityDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> ReadAGenConfigCDistrictOrCity(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new AGenConfigCDistrictOrCitySpecification(key);
      AGenConfigCDistrictOrCity district = await context.District.GetByKeyWithSpec(spec);
      return (district == null) ? NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError))
         : Ok(GeneralConfigMappingProfile.DistrictEntityToDto(district));
   }
   #endregion ReadAGenConfigCDistrictOrCity

   #region UpdateAGenConfigCDistrictOrCity
   [Route(RouteConstant.UpdateAGenConfigCDistrictOrCity)]
   [HttpPut]
   [ProducesResponseType(typeof(AGenConfigCDistrictOrCityDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> UpdateAGenConfigCDistrictOrCity(long key, AGenConfigCDistrictOrCityDto model)
   {
      if (key != model.DistrictId || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));
      var spec = new AGenConfigCDistrictOrCitySpecification(key);
      AGenConfigCDistrictOrCity district = await context.District.GetByKeyWithSpec(spec);
      if (district == null) return NotFound(new ApiResponse(404, MessageConstants.NoRecordError));
      
      if (model.DistrictName.Trim().ToLower() != district.DistrictName.Trim().ToLower()) if (await context.District.IsExists(x => x.DistrictName.Trim().ToLower() == model.DistrictName.Trim().ToLower()) == true) 
            return Conflict(new ApiResponse(409, model.DistrictName + " " + MessageConstants.DuplicateError));
      
      AGenConfigCDistrictOrCity record = GenericDataMapping.ReplaceEntityWithDto(district, model);
      context.District.UpdateEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion UpdateAGenConfigCDistrictOrCity

   #region DeleteAGenConfigCDistrictOrCity
   [Route(RouteConstant.DeleteAGenConfigCDistrictOrCity)]
   [HttpDelete]
   [ProducesResponseType(typeof(AGenConfigCDistrictOrCityDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> DeleteAGenConfigCDistrictOrCity(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordDeleteError));
      var spec = new AGenConfigCDistrictOrCityDelete(key);
      AGenConfigCDistrictOrCity record = await context.District.GetByKeyWithSpec(spec);
      if (record == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));
      if (record.AGenConfigDPoliceStations.Count() != 0) return BadRequest(new ApiResponse(400, MessageConstants.IfDeleteReffereceRecord));

      context.District.DeleteEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion AGenConfigCDistrictOrCity
}
