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

namespace pbERP.Api.Controllers.AGeneralConfig;

public class AGenConfigDPoliceStationController : BaseApiController
{
   private readonly IUnitOfWork context;

   public AGenConfigDPoliceStationController(IUnitOfWork context)
   {
      this.context = context;
   }

   #region CreateAGenConfigDPoliceStation
   [Route(RouteConstant.CreateAGenConfigDPoliceStation)]
   [HttpPost]
   [ProducesResponseType(typeof(AGenConfigDPoliceStationDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> CreateAGenConfigDPoliceStation(AGenConfigDPoliceStationDto model)
   {
      if (model == null || model.PoliceStationId != 0 || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));
      if (await context.PS.IsExists(x => x.PoliceStationName == model.PoliceStationName)) return Conflict(new ApiResponse(409, MessageConstants.DuplicateError));
      model.PoliceStationId = await context.PS.GetNextId("PoliceStationId");
      AGenConfigDPoliceStation record = GenericDataMapping.DtoToEntity<AGenConfigDPoliceStation, AGenConfigDPoliceStationDto>(model);
      context.PS.AddAsync(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion CreateAGenConfigDPoliceStation

   #region ReadAGenConfigDPoliceStations
   [Route(RouteConstant.ReadAGenConfigDPoliceStations)]
   [HttpGet]
   [ProducesResponseType(typeof(AGenConfigDPoliceStationDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadAGenConfigDPoliceStations([FromQuery] SpecificationParams specParams)
   {
      var spec = new AGenConfigDPoliceStationSpecification(specParams);
      IReadOnlyList<AGenConfigDPoliceStation> policeStations = await context.PS.ListAsyncWithSpec(spec);
      return (policeStations.Count == 0) ? BadRequest(new ApiResponse(404, MessageConstants.NoRecordError)) 
         : Ok(GeneralConfigMappingProfile.PSEntitiesToDtos(policeStations));
   }
   #endregion ReadAGenConfigDPoliceStations

   #region ReadAGenConfigDPoliceStation
   [Route(RouteConstant.ReadAGenConfigDPoliceStation)]
   [HttpGet]
   [ProducesResponseType(typeof(AGenConfigDPoliceStationDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadAGenConfigDPoliceStation(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new AGenConfigDPoliceStationSpecification(key);
      AGenConfigDPoliceStation ps = await context.PS.GetByKeyWithSpec(spec);
      return (ps == null) ? NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError))
         : Ok(GeneralConfigMappingProfile.PSEntityToDto(ps));
   }
   #endregion ReadAGenConfigDPoliceStation

   #region UpdateAGenConfigDPoliceStation
   [Route(RouteConstant.UpdateAGenConfigDPoliceStation)]
   [HttpPut]
   [ProducesResponseType(typeof(AGenConfigDPoliceStationDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> UpdateAGenConfigDPoliceStation(long key, AGenConfigDPoliceStation model)
   {
      if (key != model.PoliceStationId) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));
      AGenConfigDPoliceStation ps = await context.PS.GetFirstOrDefaultAsync(x => x.PoliceStationId == model.PoliceStationId);
      if (ps == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));
      if (ps.PoliceStationName.Trim().ToLower() != model.PoliceStationName.Trim().ToLower()) if (await context.PS.IsExists(x => x.PoliceStationName.Trim().ToLower() == model.PoliceStationName.Trim().ToLower()) == true) return Conflict(new ApiResponse(409, MessageConstants.DuplicateError));

      AGenConfigDPoliceStation record = GenericDataMapping.ReplaceEntityWithDto(ps, model);
      context.PS.UpdateEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion UpdateAGenConfigDPoliceStation

   #region DeleteAGenConfigDPoliceStation
   [Route(RouteConstant.DeleteAGenConfigDPoliceStation)]
   [HttpDelete]
   [ProducesResponseType(typeof(AGenConfigDPoliceStation), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> DeleteAGenConfigDPoliceStation(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new AGenConfigDPoliceStationSpecification(key);
      AGenConfigDPoliceStation ps = await context.PS.GetByKeyWithSpec(spec);
      if (ps == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));
      if (ps.CCompACompanies.Count != 0 || ps.DHrJEmployees.Count != 0 || ps.DHrLPresentAddresses.Count != 0 || ps.DHrMPermanentAddresses.Count != 0)
         return BadRequest(new ApiResponse(400, MessageConstants.IfDeleteReffereceRecord));
      context.PS.DeleteEntity(ps);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.DeleteFailed)) : Ok();
   }
   #endregion DeleteAGenConfigDPoliceStation

}