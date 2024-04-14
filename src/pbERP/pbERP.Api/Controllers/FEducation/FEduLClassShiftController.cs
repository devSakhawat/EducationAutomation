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

public class FEduLClassShiftController : BaseApiController
{
   private readonly IUnitOfWork context;

   public FEduLClassShiftController(IUnitOfWork context)
   {
      this.context = context;
   }

   #region CreateFEduLClassShift
   [Route(RouteConstant.CreateFEduLClassShift)]
   [HttpPost]
   [ProducesResponseType(typeof(FEduLClassShiftDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> CreateFEduLClassShift(FEduLClassShiftDto model)
   {
      if (model == null || model.ClassShiftId != 0 || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));
      if (await context.ClassShift.IsExists(x => x.ClassShiftName == model.ClassShiftName)) return Conflict(new ApiResponse(409, MessageConstants.DuplicateError));

      model.ClassShiftId = await context.ClassShift.GetNextId("ClassShiftId");
      FEduLClassShift record = GenericDataMapping.DtoToEntity<FEduLClassShift, FEduLClassShiftDto>(model);
      context.ClassShift.AddAsync(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion CreateFEduFTransportCharge

   #region ReadFEduLClassShifts
   [Route(RouteConstant.ReadFEduLClassShifts)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduLClassShiftDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduLClassShifts([FromQuery] SpecificationParams specParams)
   {
      var spec = new FEduLClassShiftSpecification(specParams);
      IReadOnlyList<FEduLClassShift> records = await context.ClassShift.ListAsyncWithSpec(spec);
      return (records.Count <= 0) ? Ok(new List<FEduLClassShiftDto>()) : Ok(GenericDataMapping.EntitiesToDtos<FEduLClassShift, FEduLClassShiftDto>(records));
   }
   #endregion ReadFEduLClassShifts

   #region ReadFEduLClassShiftByKey
   [Route(RouteConstant.ReadFEduLClassShiftByKey)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduLClassShiftDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduLClassShift(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new FEduLClassShiftSpecification(key);
      FEduLClassShift record = await context.ClassShift.GetByKeyWithSpec(spec);
      return (record == null) ? NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError)) : Ok(GenericDataMapping.EntityToDto<FEduLClassShift, FEduLClassShiftDto>(record));
   }
   #endregion ReadFEduLClassShiftByKey

   #region UpdateFEduLClassShift
   [Route(RouteConstant.UpdateFEduLClassShift)]
   [HttpPut]
   [ProducesResponseType(typeof(FEduLClassShiftDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> UpdateFEduLClassShift(long key, FEduLClassShiftDto model)
   {
      if (key != model.ClassShiftId) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));
      FEduLClassShift classShift = await context.ClassShift.GetFirstOrDefaultAsync(x => x.ClassShiftId == model.ClassShiftId);
      if (classShift == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));

      FEduLClassShift record = GenericDataMapping.ReplaceEntityWithDto(classShift, model);
      context.ClassShift.UpdateEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion UpdateFEduLClassShift

   #region DeleteFEduLClassShift
   [Route(RouteConstant.DeleteFEduLClassShift)]
   [HttpDelete]
   [ProducesResponseType(typeof(FEduLClassShiftDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> DeleteFEduLClassShift(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new FEduLClassShiftDelete(key);
      FEduLClassShift record = await context.ClassShift.GetByKeyWithSpec(spec);
      if (record == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));

      context.ClassShift.DeleteEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.DeleteFailed)) : Ok();
   }
   #endregion DeleteFEduLClassShift
}
