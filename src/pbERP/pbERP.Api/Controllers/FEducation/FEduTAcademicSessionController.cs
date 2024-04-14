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

public class FEduTAcademicSessionController : BaseApiController
{
   private readonly IUnitOfWork context;

   public FEduTAcademicSessionController(IUnitOfWork context)
   {
      this.context = context;
   }

   #region CreateFEduTAcademicSession
   [Route(RouteConstant.CreateFEduTAcademicSession)]
   [HttpPost]
   [ProducesResponseType(typeof(FEduTAcademicSessionDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> CreateFEduTAcademicSession(FEduTAcademicSessionDto model)
   {
      if (model == null || model.AcademicSessionId != 0 || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));

      model.AcademicSessionId = await context.AcademicSession.GetNextId("AcademicSessionId");
      FEduTAcademicSession record = GenericDataMapping.DtoToEntity<FEduTAcademicSession, FEduTAcademicSessionDto>(model);
      context.AcademicSession.AddAsync(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion CreateFEduFTransportCharge

   #region ReadFEduTAcademicSessions
   [Route(RouteConstant.ReadFEduTAcademicSessions)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduTAcademicSessionDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduTAcademicSessions([FromQuery] SpecificationParams specParams)
   {
      //IReadOnlyList<FEduTAcademicSession> records = await context.AcademicSession.ListAsync(predicate: x => x.AcademicSessionId);
      var spec = new FEduTAcademicSessionSpecification(specParams);
      IReadOnlyList<FEduTAcademicSession> records = await context.AcademicSession.ListAsyncWithSpec(spec);
      return (records.Count == 0) ? Ok(new List<FEduTAcademicSessionDto>()) : Ok(GenericDataMapping.EntitiesToDtos<FEduTAcademicSession, FEduTAcademicSessionDto>(records));
   }
   #endregion ReadFEduHStudentAllocateTransports

   #region ReadFEduTAcademicSessionByKey
   [Route(RouteConstant.ReadFEduTAcademicSessionByKey)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduTAcademicSessionDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduTAcademicSession(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new FEduTAcademicSessionSpecification(key);
      FEduTAcademicSession record = await context.AcademicSession.GetByKeyWithSpec(spec);
      return (record == null) ? NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError))
         : Ok(GenericDataMapping.EntityToDto<FEduTAcademicSession, FEduTAcademicSessionDto>(record));
   }
   #endregion ReadFEduTAcademicSessionByKey

   #region UpdateFEduTAcademicSession
   [Route(RouteConstant.UpdateFEduTAcademicSession)]
   [HttpPut]
   [ProducesResponseType(typeof(FEduTAcademicSessionDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> UpdateFEduTAcademicSession(long key, FEduTAcademicSessionDto model)
   {
      if (key != model.AcademicSessionId) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));
      FEduTAcademicSession academicSession = await context.AcademicSession.GetFirstOrDefaultAsync(x => x.AcademicSessionId == model.AcademicSessionId);
      if (academicSession == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));

      FEduTAcademicSession record = GenericDataMapping.ReplaceEntityWithDto(academicSession, model);
      context.AcademicSession.UpdateEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion UpdateFEduTAcademicSession

   #region DeleteFEduTAcademicSession
   [Route(RouteConstant.DeleteFEduTAcademicSession)]
   [HttpDelete]
   [ProducesResponseType(typeof(FEduTAcademicSessionDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> DeleteFEduTAcademicSession(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new FEduTAcademicSessionDelete(key);
      FEduTAcademicSession record = await context.AcademicSession.GetByKeyWithSpec(spec);
      if (record == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));

      context.AcademicSession.DeleteEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.DeleteFailed)) : Ok();
   }
   #endregion DeleteFEduTAcademicSession
}
