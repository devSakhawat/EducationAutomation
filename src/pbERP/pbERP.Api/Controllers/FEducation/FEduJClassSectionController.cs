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

public class FEduJClassSectionController : BaseApiController
{
   private readonly IUnitOfWork context;

   public FEduJClassSectionController(IUnitOfWork context)
   {
      this.context = context;
   }

   #region CreateFEduJClassSection
   [Route(RouteConstant.CreateFEduJClassSection)]
   [HttpPost]
   [ProducesResponseType(typeof(FEduJClassSectionDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> CreateFEduJClassSection(FEduJClassSectionDto model)
   {
      if (model == null || model.ClassSectionId != 0 || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));

      model.ClassSectionId = await context.ClassSection.GetNextId("ClassSectionId");
      FEduJClassSection record = GenericDataMapping.DtoToEntity<FEduJClassSection, FEduJClassSectionDto>(model);
      context.ClassSection.AddAsync(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion CreateFEduFTransportCharge

   #region ReadFEduJClassSections
   [Route(RouteConstant.ReadFEduJClassSections)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduJClassSectionDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduJClassSections([FromQuery] SpecificationParams specParams)
   {
      var spec = new FEduJClassSectionSpecification(specParams);
      IReadOnlyList<FEduJClassSection> records = await context.ClassSection.ListAsyncWithSpec(spec);
      return (records.Count() <= 0) ? Ok(new List<FEduJClassSectionDto>()) : Ok(GenericDataMapping.EntitiesToDtos<FEduJClassSection, FEduJClassSectionDto>(records));
   }
   #endregion ReadFEduJClassSections

   #region ReadFEduJClassSectionByKey
   [Route(RouteConstant.ReadFEduJClassSectionByKey)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduJClassSectionDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduJClassSection(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new FEduJClassSectionSpecification(key);
      FEduJClassSection record = await context.ClassSection.GetByKeyWithSpec(spec);
      return (record == null) ? NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError)) : Ok(GenericDataMapping.EntityToDto< FEduJClassSection, FEduJClassSectionDto>(record));
   }
   #endregion ReadFEduJClassSectionByKey

   #region UpdateFEduJClassSection
   [Route(RouteConstant.UpdateFEduJClassSection)]
   [HttpPut]
   [ProducesResponseType(typeof(FEduJClassSectionDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> UpdateFEduJClassSection(long key, FEduJClassSectionDto model)
   {
      if (key != model.ClassSectionId) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));
      FEduJClassSection classSection = await context.ClassSection.GetFirstOrDefaultAsync(x => x.ClassSectionId == model.ClassSectionId);
      if (classSection == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));

      FEduJClassSection record = GenericDataMapping.ReplaceEntityWithDto(classSection, model);
      context.ClassSection.UpdateEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion UpdateFEduJClassSection

   #region DeleteFEduJClassSection
   [Route(RouteConstant.DeleteFEduJClassSection)]
   [HttpDelete]
   [ProducesResponseType(typeof(FEduJClassSectionDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> DeleteFEduJClassSection(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new FEduJClassSectionDelete(key);
      FEduJClassSection record = await context.ClassSection.GetByKeyWithSpec(spec);
      if (record == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));

      context.ClassSection.DeleteEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.DeleteFailed)) : Ok();
   }
   #endregion DeleteFEduJClassSection
}
