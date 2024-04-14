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

public class FEduAClassController : BaseApiController
{
   private readonly IUnitOfWork context;

   public FEduAClassController(IUnitOfWork context)
   {
      this.context = context;
   }

   #region CreateFEduAClass
   [Route(RouteConstant.CreateFEduAClass)]
   [HttpPost]
   [ProducesResponseType(typeof(FEduAClassDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> CreateFEduAClass(FEduAClassDto model)
   {
      if (model == null || model.ClassId != 0 || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));
      if (await context.AClass.IsExists(x => x.ClassName == model.ClassName)) return Conflict(new ApiResponse(409, MessageConstants.DuplicateError));

      model.ClassId = await context.AClass.GetNextId("ClassId");
      FEduAClass record = GenericDataMapping.DtoToEntity<FEduAClass, FEduAClassDto>(model);
      context.AClass.AddAsync(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion CreateFEduFTransportCharge

   #region ReadFEduAClasss
   [Route(RouteConstant.ReadFEduAClasss)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduAClassDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduAClasss([FromQuery] SpecificationParams specParams)
   {
      var spec = new FEduAClassSpecification(specParams);
      IReadOnlyList<FEduAClass> records = await context.AClass.ListAsyncWithSpec(spec);
      return (records.Count == 0) ? Ok(new List<FEduAClassDto>()) : Ok(GenericDataMapping.EntitiesToDtos<FEduAClass, FEduAClassDto>(records));
   }
   #endregion ReadFEduHStudentAllocateTransports

   #region ReadFEduAClassByKey
   [Route(RouteConstant.ReadFEduAClassByKey)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduAClassDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduAClass(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new FEduAClassSpecification(key);
      FEduAClass record = await context.AClass.GetByKeyWithSpec(spec);
      return (record == null) ? NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError))
         : Ok(GenericDataMapping.EntityToDto<FEduAClass, FEduAClassDto>(record));
   }
   #endregion ReadFEduAClassByKey

   #region UpdateFEduAClass
   [Route(RouteConstant.UpdateFEduAClass)]
   [HttpPut]
   [ProducesResponseType(typeof(FEduAClassDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> UpdateFEduAClass(long key, FEduAClassDto model)
   {
      if (key != model.ClassId) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));
      FEduAClass AClass = await context.AClass.GetFirstOrDefaultAsync(x => x.ClassId == model.ClassId);
      if (AClass == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));

      FEduAClass record = GenericDataMapping.ReplaceEntityWithDto(AClass, model);
      context.AClass.UpdateEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion UpdateFEduAClass

   #region DeleteFEduAClass
   [Route(RouteConstant.DeleteFEduAClass)]
   [HttpDelete]
   [ProducesResponseType(typeof(FEduAClassDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> DeleteFEduAClass(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new FEduAClassDelete(key);
      FEduAClass record = await context.AClass.GetByKeyWithSpec(spec);
      if (record == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));

      context.AClass.DeleteEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.DeleteFailed)) : Ok();
   }
   #endregion DeleteFEduAClass
}
