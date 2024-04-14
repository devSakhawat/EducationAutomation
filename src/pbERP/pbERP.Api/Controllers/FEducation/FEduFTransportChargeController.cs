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

public class FEduFTransportChargeController : BaseApiController
{
   private readonly IUnitOfWork context;

   public FEduFTransportChargeController(IUnitOfWork context)
   {
      this.context = context;
   }

   #region CreateFEduFTransportCharge
   [Route(RouteConstant.CreateFEduFTransportCharge)]
   [HttpPost]
   [ProducesResponseType(typeof(FEduFTransportChargeDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> CreateFEduFTransportCharge(FEduFTransportChargeDto model)
   {
      if (model == null || model.TransportChargeId != 0 || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));

      model.TransportChargeId = await context.TransportCharge.GetNextId("TransportChargeId");
      FEduFTransportCharge record = GenericDataMapping.DtoToEntity<FEduFTransportCharge, FEduFTransportChargeDto>(model);
      context.TransportCharge.AddAsync(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion CreateFEduFTransportCharge

   #region ReadFEduFTransportCharges
   [Route(RouteConstant.ReadFEduFTransportCharges)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduFTransportChargeDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduFTransportCharges([FromQuery] SpecificationParams specParams)
   {
      var spec = new FEduFTransportChargeSpecification(specParams);
      IReadOnlyList<FEduFTransportCharge> records = await context.TransportCharge.ListAsyncWithSpec(spec);
      return (records.Count <= 0) ? Ok(new List<FEduFTransportChargeDto>()) : Ok(EducationMappingProfile.TransportChargeEntitiesToDtos(records));
   }
   #endregion ReadFEduFTransportCharges

   #region ReadFEduFTransportChargeByKey
   [Route(RouteConstant.ReadFEduFTransportChargeByKey)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduFTransportChargeDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduFTransportCharge(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new FEduFTransportChargeSpecification(key);
      FEduFTransportCharge record = await context.TransportCharge.GetByKeyWithSpec(spec);
      return (record == null) ? NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError)) : Ok(EducationMappingProfile.TransportChargeEntityToDto(record));
   }
   #endregion ReadFEduFTransportChargeByKey

   #region UpdateFEduFTransportCharge
   [Route(RouteConstant.UpdateFEduFTransportCharge)]
   [HttpPut]
   [ProducesResponseType(typeof(FEduFTransportChargeDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> UpdateFEduFTransportCharge(long key, FEduFTransportChargeDto model)
   {
      if (key != model.TransportChargeId) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));
      FEduFTransportCharge transportCharge = await context.TransportCharge.GetFirstOrDefaultAsync(x => x.TransportChargeId == model.TransportChargeId);
      if (transportCharge == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));

      FEduFTransportCharge record = GenericDataMapping.ReplaceEntityWithDto(transportCharge, model);
      context.TransportCharge.UpdateEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion UpdateEduCClassOrHall

   #region DeleteFEduFTransportCharge
   [Route(RouteConstant.DeleteFEduFTransportCharge)]
   [HttpDelete]
   [ProducesResponseType(typeof(FEduFTransportChargeDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> DeleteFEduFTransportCharge(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new FEduFTransportChargeDelete(key);
      FEduFTransportCharge record = await context.TransportCharge.GetByKeyWithSpec(spec);
      if (record == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));

      context.TransportCharge.DeleteEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.DeleteFailed)) : Ok();
   }
   #endregion DeleteFEduFTransportCharge
}
