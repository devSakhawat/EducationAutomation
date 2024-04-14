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

public class FEduHStudentAllocateTransportController : BaseApiController
{
   private readonly IUnitOfWork context;

   public FEduHStudentAllocateTransportController(IUnitOfWork context)
   {
      this.context = context;
   }

   #region CreateFEduHStudentAllocateTransport
   [Route(RouteConstant.CreateFEduHStudentAllocateTransport)]
   [HttpPost]
   [ProducesResponseType(typeof(FEduHStudentAllocateTransportDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> CreateFEduHStudentAllocateTransport(FEduHStudentAllocateTransportDto model)
   {
      if (model == null || model.AllocateTransportId != 0 || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));

      model.AllocateTransportId = await context.AllocateTransport.GetNextId("AllocateTransportId");
      FEduHStudentAllocateTransport record = GenericDataMapping.DtoToEntity<FEduHStudentAllocateTransport, FEduHStudentAllocateTransportDto>(model);
      context.AllocateTransport.AddAsync(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion CreateFEduFTransportCharge

   #region ReadFEduHStudentAllocateTransports
   [Route(RouteConstant.ReadFEduHStudentAllocateTransports)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduHStudentAllocateTransportDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduHStudentAllocateTransports([FromQuery] SpecificationParams specParams)
   {
      var spec = new FEduHStudentAllocateTransportSpecification(specParams);
      IReadOnlyList<FEduHStudentAllocateTransport> records = await context.AllocateTransport.ListAsyncWithSpec(spec);
      return (records.Count <= 0) ? Ok(new List<FEduFTransportChargeDto>()) : Ok(EducationMappingProfile.AllocateTransportEntitiesToDtos(records));
   }
   #endregion ReadFEduHStudentAllocateTransports

   #region ReadFEduHStudentAllocateTransportByKey
   [Route(RouteConstant.ReadFEduHStudentAllocateTransportByKey)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduHStudentAllocateTransportDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduHStudentAllocateTransport(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new FEduHStudentAllocateTransportSpecification(key);
      FEduHStudentAllocateTransport record = await context.AllocateTransport.GetByKeyWithSpec(spec);
      return (record == null) ? NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError)) : Ok(EducationMappingProfile.AllocateTransportEntityToDto(record));
   }
   #endregion ReadFEduHStudentAllocateTransportByKey

   #region UpdateFEduHStudentAllocateTransport
   [Route(RouteConstant.UpdateFEduHStudentAllocateTransport)]
   [HttpPut]
   [ProducesResponseType(typeof(FEduHStudentAllocateTransportDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> UpdateFEduHStudentAllocateTransport(long key, FEduHStudentAllocateTransportDto model)
   {
      if (key != model.TransportAreaId) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));
      FEduHStudentAllocateTransport allocateTransport = await context.AllocateTransport.GetFirstOrDefaultAsync(x => x.AllocateTransportId == model.AllocateTransportId);
      if (allocateTransport == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));

      FEduHStudentAllocateTransport record = GenericDataMapping.ReplaceEntityWithDto(allocateTransport, model);
      context.AllocateTransport.UpdateEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion UpdateFEduHStudentAllocateTransport

   #region DeleteFEduHStudentAllocateTransport
   [Route(RouteConstant.DeleteFEduHStudentAllocateTransport)]
   [HttpDelete]
   [ProducesResponseType(typeof(FEduHStudentAllocateTransportDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> DeleteFEduHStudentAllocateTransport(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new FEduHStudentAllocateTransportDelete(key);
      FEduHStudentAllocateTransport record = await context.AllocateTransport.GetByKeyWithSpec(spec);
      if (record == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));

      context.AllocateTransport.DeleteEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.DeleteFailed)) : Ok();
   }
   #endregion DeleteFEduHStudentAllocateTransport
}
