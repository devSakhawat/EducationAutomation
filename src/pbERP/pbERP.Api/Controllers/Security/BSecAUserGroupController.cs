using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pbERP.Api.Errors;
using pbERP.Api.Helpers;
using pbERP.Domain.DTOs.BSecurity;
using pbERP.Domain.Models.BSecurity;
using pbERP.Infrastructure.Constracts;
using pbERP.Infrastructure.DataMapping;
using pbERP.Infrastructure.Specifications;
using pbERP.Infrastructure.Specifications.SectionModule;
using pbERP.Utilities.Constant;

namespace pbERP.Api.Controllers.Security;

public class BSecAUserGroupController : BaseApiController
{
   private readonly IUnitOfWork context;
   private readonly IMapper mapper;

   public BSecAUserGroupController(IUnitOfWork context, IMapper mapper)
   {
      this.context = context;
      this.mapper = mapper;
   }

   #region CreateSecAUserGroup
   [Route(RouteConstant.CreateSecAUserGroup)]
   [HttpPost]
   [ProducesResponseType(typeof(BSecAUserGroupDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> CreateUserGroup(BSecAUserGroupDto model)
   {
      if (model == null || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));
      if (await context.UserGroup.IsDuplicate(x => x.UserGroupName == model.UserGroupName) == true)
         return Conflict(new ApiResponse(409, model.UserGroupName + " " + MessageConstants.DuplicateError));

      model.UserGroupId = await context.UserGroup.GetNextId("UserGroupId");
      BSecAUserGroup record = MappingSecurityModule.DtoToEntity(model);
      if (record == null) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordInsert));

      context.UserGroup.AddAsync(record);
      var saveChange = await context.SaveChangesAsync();
      if (saveChange <= 0) return BadRequest(new ApiResponse(400, MessageConstants.SaveFailed));
      return Ok();
   }
   #endregion CreateSecAUserGroup

   #region ReadSecAUserGroups
   [Route(RouteConstant.ReadSecAUserGroups)]
   [HttpGet]
   [ProducesResponseType(typeof(BSecAUserGroupDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(BSecAUserGroupDto), StatusCodes.Status404NotFound)]
   //public async Task<IActionResult> ReadUserGroups(SpecificationParams specParams)
   public async Task<IActionResult> ReadUserGroups([FromQuery] SpecificationParams specParams)
   {
      //var countSpec = new SecAUserGroupCount(specParams);
      //var spec = new SecAUserGroupSpecification(specParams);

      //var totalItems = await context.UserGroup.CountAsync(countSpec);
      //IReadOnlyList<SecAUserGroup> secAUserGroups = await context.UserGroup.ListAsyncWithSpec(spec);

      //if (secAUserGroups.Count == 0) return NotFound(new ApiResponse(404, MessageConstants.NoRecordError));
      //var data = MappingSecurityModule.SecAUserGroupsToDtos(secAUserGroups);
      //return Ok(new Pagination<SecAUserGroupDto>(specParams.PageIndex, specParams.PageSize, totalItems, data));

      IReadOnlyList<BSecAUserGroup> userGroups = await context.UserGroup.ListAsync(x => x.UserGroupId);
      if (userGroups.Count == 0) return NotFound(new ApiResponse(404, MessageConstants.NoRecordError));

      //var records = MappingSecurityModule.EntitiesToDtos(userGroups);

      IReadOnlyList<BSecAUserGroupDto> records = GenericDataMapping.EntitiesToDtos<BSecAUserGroup, BSecAUserGroupDto>(userGroups);

      return Ok(records);
   }
   //public async Task<IActionResult> ReadUserGroups([FromQuery] SpecificationParams specParams)
   //{
   //   var countSpec = new BSecAUserGroupCount(specParams);
   //   var spec = new BSecAUserGroupSpecification(specParams);

   //   var totalItems = await context.UserGroup.CountAsync(countSpec);
   //   IReadOnlyList<BSecAUserGroup> userGroups = await context.UserGroup.ListAsyncWithSpec(spec);

   //   if (userGroups.Count == 0) return NotFound(new ApiResponse(404, MessageConstants.NoRecordError));
   //   var data = GenericDataMapping.EntitiesToDtos<BSecAUserGroup, BSecAUserGroupDto>(userGroups);
   //   return Ok(new Pagination<BSecAUserGroupDto>(specParams.PageIndex, specParams.PageSize, totalItems, data));
   //}
   #endregion CreateSecAUserGroup

   #region ReadSecAUserGroupByKey
   [Route(RouteConstant.ReadSecAUserGroupByKey)]
   [HttpGet]
   [ProducesResponseType(typeof(BSecAUserGroupDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadSecAUserGroupByKey(long key)
   {
      if (key == 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new BSecAUserGroupSpecification(key);
      BSecAUserGroup userGroup = await context.UserGroup.GetByKeyWithSpec(spec);

      if (userGroup == null) return NotFound(new ApiResponse(400, MessageConstants.NoMatchFoundError));
      BSecAUserGroupDto record = mapper.Map<BSecAUserGroup, BSecAUserGroupDto>(userGroup);
      return Ok(record);
   }
   #endregion ReadSecAUserGroupByKey

   #region UpdateSecAUserGroup
   [Route(RouteConstant.UpdateSecAUserGroup)]
   [HttpPut]
   [ProducesResponseType(typeof(BSecAUserGroupDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> UpdateSecAUserGroup(long key, BSecAUserGroupDto model)
   {
      if (key == 0 || model.UserGroupId == 0 || key != model.UserGroupId) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));

      //var spec = new SecAUserGroupSpecification(key);
      //SecAUserGroup userGroup = await context.UserGroup.GetByKeyWithSpec(spec);
      BSecAUserGroup userGroup = await context.UserGroup.GetFirstOrDefaultAsync(ug => ug.UserGroupId == key);

      if (userGroup == null) return NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError));

      BSecAUserGroup record = GenericDataMapping.ReplaceEntityWithDto(userGroup, model);
      context.UserGroup.UpdateEntity(record);

      var saveChanges = await context.SaveChangesAsync();
      if (saveChanges <= 0) return BadRequest(new ApiResponse(400, MessageConstants.UpdateFailed));

      return Ok();
   }
   #endregion UpdateSecAUserGroup 

   #region DeleteSecAUserGroup
   [Route(RouteConstant.DeleteSecAUserGroup)]
   [HttpDelete]
   [ProducesResponseType(typeof(BSecAUserGroupDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> DeleteSecAUserGroup(long key)
   {
      if (key == 0) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));

      var spec = new BSecAUserGroupSpecification(key);
      BSecAUserGroup record = await context.UserGroup.GetByKeyWithSpec(spec);

      if (record.BSecBUsers.Count != 0 && record.BSecELinkUserGroupScreens.Count != 0 && record.BSecGLinkUserGroupScreenCommands.Count != 0)
         return BadRequest(new ApiResponse(400, MessageConstants.IfDeleteReffereceRecord));
      if (record == null) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordDeleteError));

      context.UserGroup.DeleteEntity(record);
      var saveChanges = await context.SaveChangesAsync();
      if (saveChanges <= 0) return BadRequest(new ApiResponse(400, MessageConstants.UpdateFailed));

      return Ok();
   }
   #endregion DeleteSecAUserGroup
}
