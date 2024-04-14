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

public class AGenConfigACountryController : BaseApiController
{
   private readonly IUnitOfWork context;

   public AGenConfigACountryController(IUnitOfWork context)
   {
      this.context = context;
   }

   #region CreateAGenConfigACountry
   [Route(RouteConstant.CreateAGenConfigACountry)]
   [HttpPost]
   [ProducesResponseType(typeof(AGenConfigACountryDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> CreateAGenConfigACountry(AGenConfigACountryDto model)
   {
      if (model == null || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));
      if (await context.Country.IsDuplicate(x => x.CountryName == model.CountryName) == true)
         return Conflict(new ApiResponse(409, model.CountryId + " " + MessageConstants.DuplicateError));

      model.CountryId = await context.Country.GetNextId("CountryId");
      AGenConfigACountry record = GenericDataMapping.DtoToEntity<AGenConfigACountry, AGenConfigACountryDto>(model);
      if (record == null) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordInsert));

      context.Country.AddAsync(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion CreateAGenConfigACountry

   #region ReadAGenConfigACountries
   [Route(RouteConstant.ReadAGenConfigACountries)]
   [HttpGet]
   [ProducesResponseType(typeof(AGenConfigACountryDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> ReadAGenConfigACountries([FromQuery] SpecificationParams specParams)
   {
      var spec = new AGenConfigACountrySpecification(specParams);
      IReadOnlyList<AGenConfigACountry> countries = await context.Country.ListAsyncWithSpec(spec);
      return (countries.Count == 0) ? BadRequest(new ApiResponse(404, MessageConstants.NoRecordError)) : Ok(GenericDataMapping.EntitiesToDtos<AGenConfigACountry, AGenConfigACountryDto>(countries));
   }
   #endregion ReadAGenConfigACountries

   #region ReadAGenConfigACountryByKey
   [Route(RouteConstant.ReadAGenConfigACountryByKey)]
   [HttpGet]
   [ProducesResponseType(typeof(AGenConfigACountryDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> ReadAGenConfigACountryByKey(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new AGenConfigACountrySpecification(key);
      AGenConfigACountry country = await context.Country.GetByKeyWithSpec(spec);
      return (country == null) ? NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError))
          : Ok(GenericDataMapping.EntityToDto<AGenConfigACountry, AGenConfigACountryDto>(country));
   }
   #endregion ReadAGenConfigEGenderByKey

   #region UpdateAGenConfigACountry
   [Route(RouteConstant.UpdateAGenConfigACountry)]
   [HttpPut]
   [ProducesResponseType(typeof(AGenConfigACountryDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> UpdateAGenConfigEGender(long key, AGenConfigACountryDto model)
   {
      if (key == 0 || key != model.CountryId) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));
      var spec = new AGenConfigACountrySpecification(key);
      AGenConfigACountry country = await context.Country.GetByKeyWithSpec(spec);
      if (country == null) return BadRequest(new ApiResponse(400, MessageConstants.NoMatchFoundError));
      AGenConfigACountry record = GenericDataMapping.ReplaceEntityWithDto(country, model);

      context.Country.UpdateEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion UpdateAGenConfigEGender

   #region DeleteAGenConfigACountry
   [Route(RouteConstant.DeleteAGenConfigACountry)]
   [HttpDelete]
   [ProducesResponseType(typeof(AGenConfigACountryDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> DeleteAGenConfigACountry(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordDeleteError));
      var spec = new AGenConfigACountryDelete(key);
      AGenConfigACountry record = await context.Country.GetByKeyWithSpec(spec);

      if (record.AGenConfigBDivisionOrStates.Count != 0 && record.AGenConfigBDivisionOrStates.Count != 0)
         return BadRequest(new ApiResponse(400, MessageConstants.IfDeleteReffereceRecord));
      if (record == null) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordDeleteError));

      context.Country.DeleteEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion DeleteAGenConfigEGender
}