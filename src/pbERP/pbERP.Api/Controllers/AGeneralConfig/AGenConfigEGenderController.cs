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

public class AGenConfigEGenderController : BaseApiController
{
   private readonly IUnitOfWork context;

   public AGenConfigEGenderController(IUnitOfWork context)
   {
      this.context = context;
   }

   #region CreateAGenConfigEGender
   [Route(RouteConstant.CreateAGenConfigEGender)]
   [HttpPost]
   [ProducesResponseType(typeof(AGenConfigEGenderDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> CreateAGenConfigEGender(AGenConfigEGenderDto model)
   {
      if (model == null || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));
      if (await context.Gender.IsDuplicate(x => x.GenderName == model.GenderName) == true)
         return Conflict(new ApiResponse(409, model.GenderId + " " + MessageConstants.DuplicateError));

      model.GenderId = await context.Gender.GetNextId("GenderId");
      AGenConfigEGender record = GenericDataMapping.DtoToEntity<AGenConfigEGender, AGenConfigEGenderDto>(model);
      if (record == null) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordInsert));

      context.Gender.AddAsync(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion CreateAGenConfigEGender

   #region ReadAGenConfigEGenders
   [Route(RouteConstant.ReadAGenConfigEGenders)]
   [HttpGet]
   [ProducesResponseType(typeof(AGenConfigEGenderDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> ReadAGenConfigEGenders([FromQuery] SpecificationParams specParams)
   {
      var spec = new AGenConfigEGenderSpecification(specParams);
      IReadOnlyList<AGenConfigEGender> genders = await context.Gender.ListAsyncWithSpec(spec);
      return (genders.Count == 0) ? BadRequest(new ApiResponse(404, MessageConstants.NoRecordError)) : Ok(GenericDataMapping.EntitiesToDtos<AGenConfigEGender, AGenConfigEGenderDto>(genders));
   }
   #endregion ReadAGenConfigIModules

   #region ReadAGenConfigEGenderByKey
   [Route(RouteConstant.ReadAGenConfigEGenderByKey)]
   [HttpGet]
   [ProducesResponseType(typeof(AGenConfigEGenderDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> ReadAGenConfigEGenderByKey(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new AGenConfigEGenderSpecification(key);

      AGenConfigEGender gender = await context.Gender.GetByKeyWithSpec(spec);
      return (gender == null) ? NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError))
         : Ok(GenericDataMapping.EntityToDto<AGenConfigEGender, AGenConfigEGenderDto>(gender));
   }
   #endregion ReadAGenConfigEGenderByKey

   #region UpdateAGenConfigEGender
   [Route(RouteConstant.UpdateAGenConfigEGender)]
   [HttpPut]
   [ProducesResponseType(typeof(AGenConfigEGenderDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> UpdateAGenConfigEGender(long key, AGenConfigEGenderDto model)
   {
      if (key == 0 || key != model.GenderId) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));
      var spec = new AGenConfigEGenderSpecification(key);
      AGenConfigEGender gender = await context.Gender.GetByKeyWithSpec(spec);
      if (gender == null) return BadRequest(new ApiResponse(400, MessageConstants.NoMatchFoundError));
      AGenConfigEGender record = GenericDataMapping.ReplaceEntityWithDto(gender, model);

      context.Gender.UpdateEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion UpdateAGenConfigEGender

   #region DeleteAGenConfigEGender
   [Route(RouteConstant.DeleteAGenConfigEGender)]
   [HttpDelete]
   [ProducesResponseType(typeof(AGenConfigEGenderDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> DeleteAGenConfigEGender(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordDeleteError));
      var spec = new AGenConfigEGenderDelete(key);
      AGenConfigEGender record = await context.Gender.GetByKeyWithSpec(spec);

      if (record.DHrJEmployees.Count != 0 && record.FEduAStudents.Count != 0)
         return BadRequest(new ApiResponse(400, MessageConstants.IfDeleteReffereceRecord));
      if (record == null) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordDeleteError));

      context.Gender.DeleteEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion DeleteAGenConfigEGender
}