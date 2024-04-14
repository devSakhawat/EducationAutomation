using Microsoft.AspNetCore.Mvc;
using pbERP.Api.Errors;
using pbERP.Domain.DTOs.FEducation;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts;
using pbERP.Infrastructure.DataMapping;
using pbERP.Infrastructure.Specifications;
using pbERP.Infrastructure.Specifications.Education;
using pbERP.Utilities.Constant;

namespace pbERP.Api.Controllers.FEducation;

public class FEduETransportAreaController : BaseApiController
{
   private readonly IUnitOfWork context;

   public FEduETransportAreaController(IUnitOfWork context)
   {
      this.context = context;
   }

   #region CreateFEduETransportArea
   [Route(RouteConstant.CreateFEduETransportArea)]
   [HttpPost]
   [ProducesResponseType(typeof(FEduETransportAreaDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> CreateFEduTransportArea(FEduETransportAreaDto model)
   {
      if (model == null || model.TransportAreaId != 0 || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));
      if (await context.TransportArea.IsExists(x => x.TransportAreaName == model.TransportAreaName)) return Conflict(new ApiResponse(409, MessageConstants.DuplicateError));

      model.TransportAreaId = await context.TransportArea.GetNextId("TransportAreaId");

      FEduETransportArea record = GenericDataMapping.DtoToEntity<FEduETransportArea, FEduETransportAreaDto>(model);
      context.TransportArea.AddAsync(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion CreateFEduETransportArea

   #region ReadFEduETransportAreas
   [Route(RouteConstant.ReadFEduETransportAreas)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduETransportAreaDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduETransportAreas([FromQuery] SpecificationParams specParams)
   {
      var spec = new FEduETransportAreaSpecification(specParams);
      IReadOnlyList<FEduETransportArea> records = await context.TransportArea.ListAsyncWithSpec(spec);
      return (records.Count() <= 0) ? Ok(new List<FEduETransportAreaDto>()) : Ok(GenericDataMapping.EntitiesToDtos<FEduETransportArea, FEduETransportAreaDto>(records));
   }
   #endregion ReadFEduETransportAreas

   #region ReadFEduETransportAreaByKey
   [Route(RouteConstant.ReadFEduETransportAreaByKey)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduETransportAreaDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduETransportArea(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new FEduETransportAreaSpecification(key);
      FEduETransportArea record = await context.TransportArea.GetByKeyWithSpec(spec);
      return (record == null) ? NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError)) : Ok(GenericDataMapping.EntityToDto<FEduETransportArea, FEduETransportAreaDto>(record));
   }
   #endregion ReadEduBClassOrHallRoomByKey

   #region UpdateFEduETransportArea
   [Route(RouteConstant.UpdateFEduETransportArea)]
   [HttpPut]
   [ProducesResponseType(typeof(FEduETransportAreaDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> UpdateFEduETransportArea(long key, FEduETransportAreaDto model)
   {
      if (key != model.TransportAreaId) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));
      FEduETransportArea transportArea = await context.TransportArea.GetFirstOrDefaultAsync(x => x.TransportAreaId == model.TransportAreaId);
      if (transportArea == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));
      if (transportArea.TransportAreaName.Trim().ToLower() != model.TransportAreaName.Trim().ToLower()) if (await context.TransportArea.IsExists(x => x.TransportAreaName.Trim().ToLower() == model.TransportAreaName.Trim().ToLower()) == true) return Conflict(new ApiResponse(409, MessageConstants.DuplicateError));

      FEduETransportArea record = GenericDataMapping.ReplaceEntityWithDto(transportArea, model);
      context.TransportArea.UpdateEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion UpdateEduCClassOrHall

   #region DeleteFEduETransportArea
   [Route(RouteConstant.DeleteFEduETransportArea)]
   [HttpDelete]
   [ProducesResponseType(typeof(FEduETransportAreaDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> DeleteFEduETransportArea(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new FEduETransportAreaDelete(key);
      FEduETransportArea record = await context.TransportArea.GetByKeyWithSpec(spec);
      if (record == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));
      if (record.FEduFTransportCharges.Count != 0 || record.FEduGLinkTransportAreas.Count != 0 || record.FEduHStudentAllocateTransports.Count != 0) return BadRequest(new ApiResponse(400, MessageConstants.IfDeleteReffereceRecord));
      context.TransportArea.DeleteEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.DeleteFailed)) : Ok();
   }
   #endregion DeleteFEduETransportArea
}
