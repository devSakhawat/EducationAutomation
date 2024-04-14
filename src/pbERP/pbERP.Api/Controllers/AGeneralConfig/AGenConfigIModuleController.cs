using Microsoft.AspNetCore.Mvc;
using pbERP.Api.Errors;
using pbERP.Domain.DTOs.AGeneralConfig;
using pbERP.Domain.Models.AGeneralConfig;
using pbERP.Infrastructure.Constracts;
using pbERP.Infrastructure.DataMapping;
using pbERP.Infrastructure.Specifications;
using pbERP.Infrastructure.Specifications.GeneralConfig;
using pbERP.Utilities.Constant;
using System.Drawing;

namespace pbERP.Api.Controllers.AGenaralConfig;

public class AGenConfigIModuleController : BaseApiController
{
   private readonly IUnitOfWork context;

   public AGenConfigIModuleController(IUnitOfWork context)
   {
      this.context = context;
   }

   #region CreateAGenConfigIModule
   [Route(RouteConstant.CreateAGenConfigIModule)]
   [HttpPost]
   [ProducesResponseType(typeof(AGenConfigIModuleDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> CreateAGenConfigIModule(AGenConfigIModuleDto model)
   {
      if (model == null || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));
      if (await context.Module.IsDuplicate(x => x.ModuleName == model.ModuleName) == true)
         return Conflict(new ApiResponse(409, model.ModuleId + " " + MessageConstants.DuplicateError));

      model.ModuleId = await context.Screen.GetNextId("ModuleId");
      AGenConfigIModule record = GenericDataMapping.DtoToEntity<AGenConfigIModule, AGenConfigIModuleDto>(model);
      if (record == null) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordInsert));

      context.Module.AddAsync(record);
      var saveChanges = await context.SaveChangesAsync();
      if (saveChanges <= 0) return BadRequest(new ApiResponse(400, MessageConstants.SaveFailed));
      return Ok();
   }
   #endregion CreateAGenConfigIModule

   #region ReadAGenConfigIModules
   [Route(RouteConstant.ReadAGenConfigIModules)]
   [HttpGet]
   [ProducesResponseType(typeof(AGenConfigIModuleDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> ReadBSecDScreens([FromQuery] SpecificationParams specParams)
   {
      //var countSpec = new AGenConfigIModuleSpecification(specParams);
      //var totalItems = await context.Module.CountAsync(countSpec);

      var spec = new AGenConfigIModuleSpecification(specParams);
      IReadOnlyList<AGenConfigIModule> modules = await context.Module.ListAsyncWithSpec(spec);
      return (modules.Count == 0) ? BadRequest(new ApiResponse(404, MessageConstants.NoRecordError)) : Ok(GenericDataMapping.EntitiesToDtos<AGenConfigIModule, AGenConfigIModuleDto>(modules));
      //return Ok(new Pagination<BSecELinkUserGroupScreenDto>(specParams.PageIndex, specParams.PageSize, totalItems, users));
   }
   #endregion ReadAGenConfigIModules

   #region ReadAGenConfigIModuleByKey
   [Route(RouteConstant.ReadAGenConfigIModuleByKey)]
   [HttpGet]
   [ProducesResponseType(typeof(AGenConfigIModuleDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> AGenConfigIModuleByKey(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new AGenConfigIModuleSpecification(key);

      AGenConfigIModule module = await context.Module.GetByKeyWithSpec(spec);
      return (module == null) ? NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError))
         : Ok(GenericDataMapping.EntityToDto<AGenConfigIModule, AGenConfigIModuleDto>(module));
   }
   #endregion ReadAGenConfigIModuleByKey

   #region UpdateAGenConfigIModule
   [Route(RouteConstant.UpdateAGenConfigIModule)]
   [HttpPut]
   [ProducesResponseType(typeof(AGenConfigIModuleDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> UpdateAGenConfigIModule(long key, AGenConfigIModuleDto model)
   {
      if (key == 0 || key != model.ModuleId) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));
      var spec = new AGenConfigIModuleSpecification(key);
      AGenConfigIModule module = await context.Module.GetByKeyWithSpec(spec);
      if (module == null) return BadRequest(new ApiResponse(400, MessageConstants.NoMatchFoundError));
      AGenConfigIModule record = GenericDataMapping.ReplaceEntityWithDto(module, model);

      context.Module.UpdateEntity(record);
      var saveChanges = await context.SaveChangesAsync();
      if (saveChanges <= 0) return BadRequest(new ApiResponse(400, MessageConstants.SaveFailed));
      return Ok();

   }
   #endregion UpdateAGenConfigIModule

   #region DeleteAGenConfigIModule
   [Route(RouteConstant.DeleteAGenConfigIModule)]
   [HttpDelete]
   [ProducesResponseType(typeof(AGenConfigIModuleDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> DeleteAGenConfigIModule(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordDeleteError));
      var spec = new AGenConfigIModuleDelete(key);
      AGenConfigIModule record = await context.Module.GetByKeyWithSpec(spec);

      if (record.AGenConfigJCompanyLinkModules.Count != 0 && record.BSecDScreens.Count != 0)
         return BadRequest(new ApiResponse(400, MessageConstants.IfDeleteReffereceRecord));
      if (record == null) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordDeleteError));

      context.Module.DeleteEntity(record);
      var saveChanges = await context.SaveChangesAsync();
      if (saveChanges <= 0) return BadRequest(new ApiResponse(400, MessageConstants.SaveFailed));

      return Ok();
   }
   #endregion DeleteAGenConfigIModule
}