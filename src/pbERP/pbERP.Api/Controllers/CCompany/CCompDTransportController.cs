using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pbERP.Api.Errors;
using pbERP.Api.Helpers;
using pbERP.Domain.DTOs.CCompany;
using pbERP.Domain.Models.CCompany;
using pbERP.Infrastructure.Constracts;
using pbERP.Infrastructure.DataMapping;
using pbERP.Infrastructure.Specifications.Company;
using pbERP.Infrastructure.Specifications;
using pbERP.Utilities.Constant;

namespace pbERP.Api.Controllers.CCompany;

public class CCompDTransportController : BaseApiController
{
   private readonly IUnitOfWork context;

   public CCompDTransportController(IUnitOfWork context)
   {
      this.context = context;
   }

   #region CreateCCompDTransport
   [Route(RouteConstant.CreateCCompDTransport)]
   [HttpPost]
   [ProducesResponseType(typeof(CCompDTransportDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> CreateCCompDTransport(CCompDTransportDto model)
   {
      if (model == null || model.TransportId != 0 || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));
      if (await context.DTransport.IsExists(x => x.TransportName == model.TransportName)) return Conflict(new ApiResponse(409, MessageConstants.DuplicateError));

      model.TransportId = await context.DTransport.GetNextId("TransportId");
      CCompDTransport record = GenericDataMapping.DtoToEntity<CCompDTransport, CCompDTransportDto>(model);
      context.DTransport.AddAsync(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion CreateFEduFTransportCharge

   #region ReadCCompDTransports
   [Route(RouteConstant.ReadCCompDTransports)]
   [HttpGet]
   [ProducesResponseType(typeof(CCompDTransportDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadCCompDTransports([FromQuery] SpecificationParams specParams)
   {
      var spec = new CCompDTransportSpecification(specParams);
      IReadOnlyList<CCompDTransport> records = await context.DTransport.ListAsyncWithSpec(spec);
      return (records.Count <= 0) ? Ok(new List<CCompDTransportDto>()) : Ok(CompanyMappingProfile.TransportEntitiesToDtos(records));
   }
   #endregion ReadFEduHStudentAllocateTransports

   #region ReadCCompDTransportByKey
   [Route(RouteConstant.ReadCCompDTransportByKey)]
   [HttpGet]
   [ProducesResponseType(typeof(CCompDTransportDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadCCompDTransport(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new CCompDTransportSpecification(key);
      CCompDTransport record = await context.DTransport.GetByKeyWithSpec(spec);
      return (record == null) ? NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError)) : Ok(CompanyMappingProfile.TrasnportEntityToDto(record));
   }
   #endregion ReadCCompDTransportByKey

   #region UpdateCCompDTransport
   [Route(RouteConstant.UpdateCCompDTransport)]
   [HttpPut]
   [ProducesResponseType(typeof(CCompDTransportDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> UpdateCCompDTransport(long key, CCompDTransportDto model)
   {
      if (key != model.TransportId) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));
      CCompDTransport DTransport = await context.DTransport.GetFirstOrDefaultAsync(x => x.TransportId == model.TransportId);
      if (DTransport == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));

      CCompDTransport record = GenericDataMapping.ReplaceEntityWithDto(DTransport, model);
      context.DTransport.UpdateEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion UpdateCCompDTransport

   #region DeleteCCompDTransport
   [Route(RouteConstant.DeleteCCompDTransport)]
   [HttpDelete]
   [ProducesResponseType(typeof(CCompDTransportDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> DeleteCCompDTransport(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new CCompDTransportDelete(key);
      CCompDTransport record = await context.DTransport.GetByKeyWithSpec(spec);
      if (record == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));

      context.DTransport.DeleteEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.DeleteFailed)) : Ok();
   }
   #endregion DeleteCCompDTransport
}
