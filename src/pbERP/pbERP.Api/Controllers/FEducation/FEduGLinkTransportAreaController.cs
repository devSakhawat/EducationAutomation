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

public class FEduGLinkTransportAreaController : BaseApiController
{
   private readonly IUnitOfWork context;

   public FEduGLinkTransportAreaController(IUnitOfWork context)
   {
      this.context = context;
   }

   #region CreateFEduGLinkTransportArea
   [Route(RouteConstant.CreateFEduGLinkTransportArea)]
   [HttpPost]
   [ProducesResponseType(typeof(FEduGLinkTransportAreaDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> CreateFEduTransportArea(List<FEduGLinkTransportAreaDto> models)
   {
      if (models.Count() == 0 || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));

      int results = await context.LinkTransportArea.BulkInsertLinkTransportArea(models);
      return (results != 200) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();

      //var records = models.Select(x => new FEduGLinkTransportArea 
      //{ LinkAreaTransportId = linkAreaTransportId++, TransportId = x.TransportId, TransportAreaId = x.TransportAreaId });

      //FEduGLinkTransportArea record = GenericDataMapping.DtoToEntity<FEduGLinkTransportArea, FEduGLinkTransportAreaDto>(model);
      //context.LinkTransportArea.AddAsync(record);
      //return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion CreateFEduFTransportCharge

   #region ReadFEduGLinkTransportAreas
   [Route(RouteConstant.ReadFEduGLinkTransportAreas)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduFTransportChargeDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduGLinkTransportAreas([FromQuery] SpecificationParams specParams)
   {
      var spec = new FEduGLinkTransportAreaSpecification(specParams);
      IReadOnlyList<FEduGLinkTransportArea> records = await context.LinkTransportArea.ListAsyncWithSpec(spec);
      return (records.Count <= 0) ? Ok(new List<FEduFTransportChargeDto>()) : Ok(EducationMappingProfile.LinkTransportEntitiesToDtos(records));
   }
   #endregion ReadFEduGLinkTransportAreas

   #region ReadFEduGLinkTransportAreaByKey
   [Route(RouteConstant.ReadFEduGLinkTransportAreaByKey)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduGLinkTransportAreaDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduGLinkTransportArea(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new FEduGLinkTransportAreaSpecification(key);
      FEduGLinkTransportArea record = await context.LinkTransportArea.GetByKeyWithSpec(spec);
      return (record == null) ? NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError)) : Ok(EducationMappingProfile.LinkTransportEntityToDto(record));
   }
   #endregion ReadFEduGLinkTransportAreaByKey

   #region UpdateFEduGLinkTransportArea
   [Route(RouteConstant.UpdateFEduGLinkTransportArea)]
   [HttpPut]
   [ProducesResponseType(typeof(FEduGLinkTransportAreaDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> UpdateFEduGLinkTransportArea(long key, FEduGLinkTransportAreaDto model)
   {
      if (key != model.TransportAreaId) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));
      FEduGLinkTransportArea linkTransportArea = await context.LinkTransportArea.GetFirstOrDefaultAsync(x => x.LinkAreaTransportId == model.LinkAreaTransportId);
      if (linkTransportArea == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));

      FEduGLinkTransportArea record = GenericDataMapping.ReplaceEntityWithDto(linkTransportArea, model);
      context.LinkTransportArea.UpdateEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion UpdateFEduGLinkTransportArea

   #region DeleteFEduGLinkTransportArea
   [Route(RouteConstant.DeleteFEduGLinkTransportArea)]
   [HttpDelete]
   [ProducesResponseType(typeof(FEduGLinkTransportAreaDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> DeleteFEduGLinkTransportArea(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new FEduGLinkTransportAreaDelete(key);
      FEduGLinkTransportArea record = await context.LinkTransportArea.GetByKeyWithSpec(spec);
      if (record == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));

      context.LinkTransportArea.DeleteEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.DeleteFailed)) : Ok();
   }
   #endregion DeleteFEduGLinkTransportArea
}
