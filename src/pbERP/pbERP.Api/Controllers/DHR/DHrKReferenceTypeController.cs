using Microsoft.AspNetCore.Mvc;
using pbERP.Api.Errors;
using pbERP.Domain.DTOs.DHR;
using pbERP.Domain.Models.DHR;
using pbERP.Infrastructure.Constracts;
using pbERP.Infrastructure.DataMapping;
using pbERP.Infrastructure.Specifications;
using pbERP.Infrastructure.Specifications.HR;
using pbERP.Utilities.Constant;

namespace pbERP.Api.Controllers.AGenaralConfig;

public class DHrKReferenceTypeController : BaseApiController
{
   private readonly IUnitOfWork context;

   public DHrKReferenceTypeController(IUnitOfWork context)
   {
      this.context = context;
   }

   #region CreateDHrKReferenceType
   [Route(RouteConstant.CreateDHrKReferenceType)]
   [HttpPost]
   [ProducesResponseType(typeof(DHrKReferenceTypeDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> CreateDHrKReferenceType(DHrKReferenceTypeDto model)
   {
      if (model == null || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));
      if (await context.DHrKReferenceType.IsDuplicate(x => x.ReferenceTypeName == model.ReferenceTypeName) == true)
         return Conflict(new ApiResponse(409, model.ReferenceTypeName + " " + MessageConstants.DuplicateError));

      model.ReferenceTypeId = await context.DHrKReferenceType.GetNextId("ReferenceTypeId");
      DHrKReferenceType record = GenericDataMapping.DtoToEntity<DHrKReferenceType, DHrKReferenceTypeDto>(model);
      if (record == null) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordInsert));

      context.DHrKReferenceType.AddAsync(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion CreateDHrKReferenceType

   #region ReadDHrKReferenceTypes
   [Route(RouteConstant.ReadDHrKReferenceTypes)]
   [HttpGet]
   [ProducesResponseType(typeof(DHrKReferenceTypeDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> ReadDHrKReferenceTypes([FromQuery] SpecificationParams specParams)
   {
      var spec = new DHrKReferenceTypeSpecification(specParams);
      IReadOnlyList<DHrKReferenceType> referenceTypes = await context.DHrKReferenceType.ListAsyncWithSpec(spec);
      return (referenceTypes.Count == 0) ? BadRequest(new ApiResponse(404, MessageConstants.NoRecordError)) : Ok(GenericDataMapping.EntitiesToDtos<DHrKReferenceType, DHrKReferenceTypeDto>(referenceTypes));
   }
   #endregion ReadDHrKReferenceTypes

   #region ReadDHrKReferenceTypeByKey
   [Route(RouteConstant.ReadDHrKReferenceTypeByKey)]
   [HttpGet]
   [ProducesResponseType(typeof(DHrKReferenceTypeDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> ReadDHrKReferenceTypeByKey(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new DHrKReferenceTypeSpecification(key);

      DHrKReferenceType refereceType = await context.DHrKReferenceType.GetByKeyWithSpec(spec);
      return (refereceType == null) ? NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError))
         : Ok(GenericDataMapping.EntityToDto<DHrKReferenceType, DHrKReferenceTypeDto>(refereceType));
   }
   #endregion ReadDHrKReferenceTypeByKey

   #region UpdateDHrKReferenceType
   [Route(RouteConstant.UpdateDHrKReferenceType)]
   [HttpPut]
   [ProducesResponseType(typeof(DHrKReferenceTypeDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> UpdateDHrKReferenceType(long key, DHrKReferenceTypeDto model)
   {
      if (key == 0 || key != model.ReferenceTypeId) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));
      var spec = new DHrKReferenceTypeSpecification(key);
      DHrKReferenceType refereceType = await context.DHrKReferenceType.GetByKeyWithSpec(spec);
      if (refereceType == null) return BadRequest(new ApiResponse(400, MessageConstants.NoMatchFoundError));
      DHrKReferenceType record = GenericDataMapping.ReplaceEntityWithDto(refereceType, model);

      context.DHrKReferenceType.UpdateEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion UpdateDHrKReferenceType

   #region DeleteDHrKReferenceType
   [Route(RouteConstant.DeleteDHrKReferenceType)]
   [HttpDelete]
   [ProducesResponseType(typeof(DHrKReferenceTypeDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> DeleteDHrKReferenceType(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordDeleteError));
      var spec = new DHrKReferenceTypeDelete(key);
      DHrKReferenceType record = await context.DHrKReferenceType.GetByKeyWithSpec(spec);

      if (record.DHrLPresentAddresses.Count != 0 && record.DHrMPermanentAddresses.Count != 0)
         return BadRequest(new ApiResponse(400, MessageConstants.IfDeleteReffereceRecord));
      if (record == null) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordDeleteError));

      context.DHrKReferenceType.DeleteEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion DeleteDHrKReferenceType
}