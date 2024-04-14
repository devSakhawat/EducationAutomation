using Microsoft.AspNetCore.Mvc;
using pbERP.Api.Errors;
using pbERP.Domain.DTOs.AGeneralConfig;
using pbERP.Domain.Models.AGeneralConfig;
using pbERP.Infrastructure.Constracts;
using pbERP.Infrastructure.DataMapping;
using pbERP.Infrastructure.Specifications;
using pbERP.Infrastructure.Specifications.GeneralConfig;
using pbERP.Utilities.Constant;

namespace pbERP.Api.Controllers.AGenaralConfig;

public class AGenConfigGReligionController : BaseApiController
{
   private readonly IUnitOfWork context;

   public AGenConfigGReligionController(IUnitOfWork context)
   {
      this.context = context;
   }

   #region CreateAGenConfigGReligion
   [Route(RouteConstant.CreateAGenConfigGReligion)]
   [HttpPost]
   [ProducesResponseType(typeof(AGenConfigGReligionDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> CreateAGenConfigGReligion(AGenConfigGReligionDto model)
   {
      if (model == null || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));
      if (await context.Religion.IsDuplicate(x => x.ReligionName == model.ReligionName) == true)
         return Conflict(new ApiResponse(409, model.ReligionId + " " + MessageConstants.DuplicateError));

      model.ReligionId = await context.Religion.GetNextId("ReligionId");
      AGenConfigGReligion record = GenericDataMapping.DtoToEntity<AGenConfigGReligion, AGenConfigGReligionDto>(model);
      if (record == null) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordInsert));

      context.Religion.AddAsync(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion CreateAGenConfigGReligion

   #region ReadAGenConfigGReligions
   [Route(RouteConstant.ReadAGenConfigGReligions)]
   [HttpGet]
   [ProducesResponseType(typeof(AGenConfigGReligionDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> ReadAGenConfigGReligions([FromQuery] SpecificationParams specParams)
   {
      var spec = new AGenConfigGReligionSpecification(specParams);
      IReadOnlyList<AGenConfigGReligion> religions = await context.Religion.ListAsyncWithSpec(spec);
      return (religions.Count == 0) ? BadRequest(new ApiResponse(404, MessageConstants.NoRecordError)) : Ok(GenericDataMapping.EntitiesToDtos<AGenConfigGReligion, AGenConfigGReligionDto>(religions));
   }
   #endregion ReadAGenConfigGReligions

   #region ReadAGenConfigGReligionByKey
   [Route(RouteConstant.ReadAGenConfigGReligionByKey)]
   [HttpGet]
   [ProducesResponseType(typeof(AGenConfigGReligionDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> ReadAGenConfigGReligionByKey(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new AGenConfigGReligionSpecification(key);

      AGenConfigGReligion religion = await context.Religion.GetByKeyWithSpec(spec);
      return (religion == null) ? NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError))
         : Ok(GenericDataMapping.EntityToDto<AGenConfigGReligion, AGenConfigGReligionDto>(religion));
   }
   #endregion ReadAGenConfigGReligionByKey

   #region UpdateAGenConfigGReligion
   [Route(RouteConstant.UpdateAGenConfigGReligion)]
   [HttpPut]
   [ProducesResponseType(typeof(AGenConfigGReligionDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> UpdateAGenConfigGReligion(long key, AGenConfigGReligionDto model)
   {
      if (key == 0 || key != model.ReligionId) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));
      var spec = new AGenConfigGReligionSpecification(key);
      AGenConfigGReligion religion = await context.Religion.GetByKeyWithSpec(spec);
      if (religion == null) return BadRequest(new ApiResponse(400, MessageConstants.NoMatchFoundError));
      AGenConfigGReligion record = GenericDataMapping.ReplaceEntityWithDto(religion, model);

      context.Religion.UpdateEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion UpdateAGenConfigGReligion

   #region DeleteAGenConfigGReligion
   [Route(RouteConstant.DeleteAGenConfigGReligion)]
   [HttpDelete]
   [ProducesResponseType(typeof(AGenConfigGReligionDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> DeleteAGenConfigGReligion(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordDeleteError));
      var spec = new AGenConfigGReligionDelete(key);
      AGenConfigGReligion record = await context.Religion.GetByKeyWithSpec(spec);

      if (record.DHrJEmployees.Count != 0 && record.FEduAStudents.Count != 0)
         return BadRequest(new ApiResponse(400, MessageConstants.IfDeleteReffereceRecord));
      if (record == null) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordDeleteError));

      context.Religion.DeleteEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion DeleteAGenConfigGReligion
}