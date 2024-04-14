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

public class FEduRExamController : BaseApiController
{
   private readonly IUnitOfWork context;

   public FEduRExamController(IUnitOfWork context)
   {
      this.context = context;
   }

   #region CreateFEduRExam
   [Route(RouteConstant.CreateFEduRExam)]
   [HttpPost]
   [ProducesResponseType(typeof(FEduRExamDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> CreateFEduRExam(FEduRExamDto model)
   {
      if (model == null || model.ExamId != 0 || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));
      if (await context.Exam.IsExists(x => x.ExamName == model.ExamName)) return Conflict(new ApiResponse(409, MessageConstants.DuplicateError));

      model.ExamId = await context.Exam.GetNextId("ExamId");
      FEduRExam record = GenericDataMapping.DtoToEntity<FEduRExam, FEduRExamDto>(model);
      context.Exam.AddAsync(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion CreateFEduFTransportCharge

   #region ReadFEduRExams
   [Route(RouteConstant.ReadFEduRExams)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduRExamDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduRExams([FromQuery] SpecificationParams specParams)
   {
      var spec = new FEduRExamSpecification(specParams);
      IReadOnlyList<FEduRExam> records = await context.Exam.ListAsyncWithSpec(spec);
      return (records.Count <= 0) ? Ok(new List<FEduRExamDto>()) : Ok(GenericDataMapping.EntitiesToDtos<FEduRExam, FEduRExamDto>(records));
   }
   #endregion ReadFEduHStudentAllocateTransports

   #region ReadFEduRExamByKey
   [Route(RouteConstant.ReadFEduRExamByKey)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduRExamDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduRExam(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new FEduRExamSpecification(key);
      FEduRExam record = await context.Exam.GetByKeyWithSpec(spec);
      return (record == null) ? NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError))
         : Ok(GenericDataMapping.EntityToDto<FEduRExam, FEduRExamDto>(record));
   }
   #endregion ReadFEduRExamByKey

   #region UpdateFEduRExam
   [Route(RouteConstant.UpdateFEduRExam)]
   [HttpPut]
   [ProducesResponseType(typeof(FEduRExamDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> UpdateFEduRExam(long key, FEduRExamDto model)
   {
      if (key != model.ExamId) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));
      FEduRExam exam = await context.Exam.GetFirstOrDefaultAsync(x => x.ExamId == model.ExamId);
      if (exam == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));

      FEduRExam record = GenericDataMapping.ReplaceEntityWithDto(exam, model);
      context.Exam.UpdateEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion UpdateFEduRExam

   #region DeleteFEduRExam
   [Route(RouteConstant.DeleteFEduRExam)]
   [HttpDelete]
   [ProducesResponseType(typeof(FEduRExamDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> DeleteFEduRExam(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new FEduRExamDelete(key);
      FEduRExam record = await context.Exam.GetByKeyWithSpec(spec);
      if (record == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));

      context.Exam.DeleteEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.DeleteFailed)) : Ok();
   }
   #endregion DeleteFEduRExam
}
