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

public class FEduUClassPeriodController : BaseApiController
{
   private readonly IUnitOfWork context;

   public FEduUClassPeriodController(IUnitOfWork context)
   {
      this.context = context;
   }

   #region CreateFEduUClassPeriod
   [Route(RouteConstant.CreateFEduUClassPeriod)]
   [HttpPost]
   [ProducesResponseType(typeof(FEduUClassPeriodDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> CreateFEduUClassPeriod(FEduUClassPeriodDto model)
   {
      if (model == null || model.ClassPeriodId != 0 || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));

      model.ClassPeriodId = await context.ClassPeriod.GetNextId("ClassPeriodId");
      FEduUClassPeriod record = GenericDataMapping.DtoToEntity<FEduUClassPeriod, FEduUClassPeriodDto>(model);
      context.ClassPeriod.AddAsync(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion CreateFEduFTransportCharge

   #region ReadFEduUClassPeriods
   [Route(RouteConstant.ReadFEduUClassPeriods)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduUClassPeriodDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduUClassPeriods([FromQuery] SpecificationParams specParams)
   {
      var spec = new FEduUClassPeriodSpecification(specParams);
      IReadOnlyList<FEduUClassPeriod> records = await context.ClassPeriod.ListAsyncWithSpec(spec);
      return (records.Count <= 0) ? Ok(new List<FEduUClassPeriodDto>()) : Ok(GenericDataMapping.EntitiesToDtos<FEduUClassPeriod, FEduUClassPeriodDto>(records));
   }
   #endregion ReadFEduHStudentAllocateTransports

   #region ReadFEduUClassPeriodByKey
   [Route(RouteConstant.ReadFEduUClassPeriodByKey)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduUClassPeriodDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduUClassPeriod(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new FEduUClassPeriodSpecification(key);
      FEduUClassPeriod record = await context.ClassPeriod.GetByKeyWithSpec(spec);
      return (record == null) ? NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError))
         : Ok(GenericDataMapping.EntityToDto<FEduUClassPeriod, FEduUClassPeriodDto>(record));
   }
   #endregion ReadFEduUClassPeriodByKey

   #region UpdateFEduUClassPeriod
   [Route(RouteConstant.UpdateFEduUClassPeriod)]
   [HttpPut]
   [ProducesResponseType(typeof(FEduUClassPeriodDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> UpdateFEduUClassPeriod(long key, FEduUClassPeriodDto model)
   {
      if (key != model.ClassPeriodId) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));
      FEduUClassPeriod classPeriod = await context.ClassPeriod.GetFirstOrDefaultAsync(x => x.ClassPeriodId == model.ClassPeriodId);
      if (classPeriod == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));

      FEduUClassPeriod record = GenericDataMapping.ReplaceEntityWithDto(classPeriod, model);
      context.ClassPeriod.UpdateEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion UpdateFEduUClassPeriod

   #region DeleteFEduUClassPeriod
   [Route(RouteConstant.DeleteFEduUClassPeriod)]
   [HttpDelete]
   [ProducesResponseType(typeof(FEduUClassPeriodDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> DeleteFEduUClassPeriod(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new FEduUClassPeriodDelete(key);
      FEduUClassPeriod record = await context.ClassPeriod.GetByKeyWithSpec(spec);
      if (record == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));

      context.ClassPeriod.DeleteEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.DeleteFailed)) : Ok();
   }
   #endregion DeleteFEduUClassPeriod
}
