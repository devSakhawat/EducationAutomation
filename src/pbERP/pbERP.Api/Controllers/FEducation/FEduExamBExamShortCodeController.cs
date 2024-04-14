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

public class FEduExamBExamShortCodeController : BaseApiController
{
   private readonly IUnitOfWork context;

   public FEduExamBExamShortCodeController(IUnitOfWork context)
   {
      this.context = context;
   }

   #region CreateFEduExamBExamShortCode
   [Route(RouteConstant.CreateFEduExamBExamShortCode)]
   [HttpPost]
   [ProducesResponseType(typeof(FEduExamBExamShortCodeDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> CreateFEduExamBExamShortCode(FEduExamBExamShortCodeDto model)
   {
      if (model == null || model.ExamShortCodeId != 0 || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));
      if (await context.ExamShortCode.IsExists(x => x.ExamShortCode == model.ExamShortCode)) return Conflict(new ApiResponse(409, MessageConstants.DuplicateError));

      model.ExamShortCodeId = await context.ExamShortCode.GetNextId("ExamShortCodeId");
      FEduExamBExamShortCode record = GenericDataMapping.DtoToEntity<FEduExamBExamShortCode, FEduExamBExamShortCodeDto>(model);
      context.ExamShortCode.AddAsync(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion CreateFEduFTransportCharge

   #region ReadFEduExamBExamShortCodes
   [Route(RouteConstant.ReadFEduExamBExamShortCodes)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduExamBExamShortCodeDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduExamBExamShortCodes([FromQuery] SpecificationParams specParams)
   {
      var spec = new FEduExamBExamShortCodeSpecification(specParams);
      IReadOnlyList<FEduExamBExamShortCode> records = await context.ExamShortCode.ListAsyncWithSpec(spec);
      return (records.Count <= 0) ? Ok(new List<FEduExamBExamShortCodeDto>()) : Ok(GenericDataMapping.EntitiesToDtos<FEduExamBExamShortCode, FEduExamBExamShortCodeDto>(records));
   }
   #endregion ReadFEduHStudentAllocateTransports

   #region ReadFEduExamBExamShortCodeByKey
   [Route(RouteConstant.ReadFEduExamBExamShortCodeByKey)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduExamBExamShortCodeDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduExamBExamShortCode(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new FEduExamBExamShortCodeSpecification(key);
      FEduExamBExamShortCode record = await context.ExamShortCode.GetByKeyWithSpec(spec);
      return (record == null) ? NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError))
         : Ok(GenericDataMapping.EntityToDto<FEduExamBExamShortCode, FEduExamBExamShortCodeDto>(record));
   }
   #endregion ReadFEduExamBExamShortCodeByKey

   #region UpdateFEduExamBExamShortCode
   [Route(RouteConstant.UpdateFEduExamBExamShortCode)]
   [HttpPut]
   [ProducesResponseType(typeof(FEduExamBExamShortCodeDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> UpdateFEduExamBExamShortCode(long key, FEduExamBExamShortCodeDto model)
   {
      if (key != model.ExamShortCodeId) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));
      FEduExamBExamShortCode exam = await context.ExamShortCode.GetFirstOrDefaultAsync(x => x.ExamShortCode == model.ExamShortCode);
      if (exam == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));

      FEduExamBExamShortCode record = GenericDataMapping.ReplaceEntityWithDto(exam, model);
      context.ExamShortCode.UpdateEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion UpdateFEduExamBExamShortCode

   #region DeleteFEduExamBExamShortCode
   [Route(RouteConstant.DeleteFEduExamBExamShortCode)]
   [HttpDelete]
   [ProducesResponseType(typeof(FEduExamBExamShortCodeDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> DeleteFEduExamBExamShortCode(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new FEduExamBExamShortCodeDelete(key);
      FEduExamBExamShortCode record = await context.ExamShortCode.GetByKeyWithSpec(spec);
      if (record == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));

      context.ExamShortCode.DeleteEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.DeleteFailed)) : Ok();
   }
   #endregion DeleteFEduExamBExamShortCode
}
