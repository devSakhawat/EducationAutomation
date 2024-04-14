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

public class FEduSAcademicYearController : BaseApiController
{
   private readonly IUnitOfWork context;

   public FEduSAcademicYearController(IUnitOfWork context)
   {
      this.context = context;
   }

   #region CreateFEduSAcademicYear
   [Route(RouteConstant.CreateFEduSAcademicYear)]
   [HttpPost]
   [ProducesResponseType(typeof(FEduSAcademicYearDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> CreateFEduSAcademicYear(FEduSAcademicYearDto model)
   {
      if (model == null || model.AcademicYearId != 0 || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));
      if (await context.AcademicYear.IsExists(x => x.AcademicYear.Trim() == model.AcademicYear.Trim())) return Conflict(new ApiResponse(409, MessageConstants.DuplicateError));

      model.AcademicYearId = await context.AcademicYear.GetNextId("AcademicYearId");
      FEduSAcademicYear record = GenericDataMapping.DtoToEntity<FEduSAcademicYear, FEduSAcademicYearDto>(model);
      context.AcademicYear.AddAsync(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion CreateFEduFTransportCharge

   #region ReadFEduSAcademicYears
   [Route(RouteConstant.ReadFEduSAcademicYears)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduSAcademicYearDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduSAcademicYears([FromQuery] SpecificationParams specParams)
   {
      var spec = new FEduSAcademicYearSpecification(specParams);
      IReadOnlyList<FEduSAcademicYear> records = await context.AcademicYear.ListAsyncWithSpec(spec);
      return (records.Count <= 0) ? Ok(new List<FEduSAcademicYearDto>()) : Ok(GenericDataMapping.EntitiesToDtos<FEduSAcademicYear, FEduSAcademicYearDto>(records));
   }
   #endregion ReadFEduHStudentAllocateTransports

   #region ReadFEduSAcademicYearByKey
   [Route(RouteConstant.ReadFEduSAcademicYearByKey)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduSAcademicYearDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduSAcademicYear(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new FEduSAcademicYearSpecification(key);
      FEduSAcademicYear record = await context.AcademicYear.GetByKeyWithSpec(spec);
      return (record == null) ? NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError))
         : Ok(GenericDataMapping.EntityToDto<FEduSAcademicYear, FEduSAcademicYearDto>(record));
   }
   #endregion ReadFEduSAcademicYearByKey

   #region UpdateFEduSAcademicYear
   [Route(RouteConstant.UpdateFEduSAcademicYear)]
   [HttpPut]
   [ProducesResponseType(typeof(FEduSAcademicYearDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> UpdateFEduSAcademicYear(long key, FEduSAcademicYearDto model)
   {
      if (key != model.AcademicYearId) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));
      FEduSAcademicYear academicYear = await context.AcademicYear.GetFirstOrDefaultAsync(x => x.AcademicYearId == model.AcademicYearId);
      if (academicYear == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));

      FEduSAcademicYear record = GenericDataMapping.ReplaceEntityWithDto(academicYear, model);
      context.AcademicYear.UpdateEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion UpdateFEduSAcademicYear

   #region DeleteFEduSAcademicYear
   [Route(RouteConstant.DeleteFEduSAcademicYear)]
   [HttpDelete]
   [ProducesResponseType(typeof(FEduSAcademicYearDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> DeleteFEduSAcademicYear(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new FEduSAcademicYearDelete(key);
      FEduSAcademicYear record = await context.AcademicYear.GetByKeyWithSpec(spec);
      if (record == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));

      context.AcademicYear.DeleteEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.DeleteFailed)) : Ok();
   }
   #endregion DeleteFEduSAcademicYear
}
