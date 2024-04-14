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

public class FEduMClassSubjectController : BaseApiController
{
   private readonly IUnitOfWork context;

   public FEduMClassSubjectController(IUnitOfWork context)
   {
      this.context = context;
   }

   #region CreateFEduMClassSubject
   [Route(RouteConstant.CreateFEduMClassSubject)]
   [HttpPost]
   [ProducesResponseType(typeof(FEduMClassSubjectDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> CreateFEduMClassSubject(FEduMClassSubjectDto model)
   {
      if (model == null || model.ClassSubjectId != 0 || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));
      if (await context.ClassSubject.IsExists(x => x.ClassSubjectName == model.ClassSubjectName)) return Conflict(new ApiResponse(409, MessageConstants.DuplicateError));

      model.ClassSubjectId = await context.ClassSubject.GetNextId("ClassSubjectId");
      FEduMClassSubject record = GenericDataMapping.DtoToEntity<FEduMClassSubject, FEduMClassSubjectDto>(model);
      context.ClassSubject.AddAsync(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion CreateFEduFTransportCharge

   #region ReadFEduMClassSubjects
   [Route(RouteConstant.ReadFEduMClassSubjects)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduMClassSubjectDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduMClassSubjects([FromQuery] SpecificationParams specParams)
   {
      var spec = new FEduMClassSubjectSpecification(specParams);
      IReadOnlyList<FEduMClassSubject> records = await context.ClassSubject.ListAsyncWithSpec(spec);
      return (records.Count <= 0) ? Ok(new List<FEduMClassSubjectDto>()) : Ok(GenericDataMapping.EntitiesToDtos<FEduMClassSubject, FEduMClassSubjectDto>(records));
   }
   #endregion ReadFEduHStudentAllocateTransports

   #region ReadFEduMClassSubjectByKey
   [Route(RouteConstant.ReadFEduMClassSubjectByKey)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduMClassSubjectDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduMClassSubject(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new FEduMClassSubjectSpecification(key);
      FEduMClassSubject record = await context.ClassSubject.GetByKeyWithSpec(spec);
      return (record == null) ? NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError))
         : Ok(GenericDataMapping.EntityToDto<FEduMClassSubject, FEduMClassSubjectDto>(record));
   }
   #endregion ReadFEduMClassSubjectByKey

   #region UpdateFEduMClassSubject
   [Route(RouteConstant.UpdateFEduMClassSubject)]
   [HttpPut]
   [ProducesResponseType(typeof(FEduMClassSubjectDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> UpdateFEduMClassSubject(long key, FEduMClassSubjectDto model)
   {
      if (key != model.ClassSubjectId) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));
      FEduMClassSubject classSubject = await context.ClassSubject.GetFirstOrDefaultAsync(x => x.ClassSubjectId == model.ClassSubjectId);
      if (classSubject == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));

      FEduMClassSubject record = GenericDataMapping.ReplaceEntityWithDto(classSubject, model);
      context.ClassSubject.UpdateEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion UpdateFEduMClassSubject

   #region DeleteFEduMClassSubject
   [Route(RouteConstant.DeleteFEduMClassSubject)]
   [HttpDelete]
   [ProducesResponseType(typeof(FEduMClassSubjectDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> DeleteFEduMClassSubject(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new FEduMClassSubjectDelete(key);
      FEduMClassSubject record = await context.ClassSubject.GetByKeyWithSpec(spec);
      if (record == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));

      context.ClassSubject.DeleteEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.DeleteFailed)) : Ok();
   }
   #endregion DeleteFEduMClassSubject
}
