using Microsoft.AspNetCore.Mvc;
using pbERP.Api.Errors;
using pbERP.Api.Helpers;
using pbERP.Domain.DTOs.BSecurity;
using pbERP.Domain.Models.BSecurity;
using pbERP.Infrastructure.Constracts;
using pbERP.Infrastructure.DataMapping;
using pbERP.Infrastructure.Specifications;
using pbERP.Infrastructure.Specifications.SecurityModule;
using pbERP.Utilities.Constant;

namespace pbERP.Api.Controllers.Security;

public class BSecBUserController : BaseApiController
{
   private readonly IUnitOfWork context;

   public BSecBUserController(IUnitOfWork context)
   {
      this.context = context;
   }

   #region CreateSecBUser
   [Route(RouteConstant.CreateSecBUser)]
   [HttpPost]
   [ProducesResponseType(typeof(BSecBUserDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> CreateSecBUser(BSecBUserDto model)
   {
      if (model == null || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));
      if (await context.UserGroup.IsDuplicate(x => x.UserGroupName == model.LoginName) == true)
         return Conflict(new ApiResponse(409, model.LoginName   + " " + MessageConstants.DuplicateError));

      model.UserId = await context.User.GetNextId("UserId");
      BSecBUser record = GenericDataMapping.DtoToEntity<BSecBUser, BSecBUserDto>(model);
      if (record == null) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordInsert));


      context.User.AddAsync(record);
      var saveChanges = await context.SaveChangesAsync();
      if (saveChanges <= 0) return BadRequest(new ApiResponse(400, MessageConstants.SaveFailed));
      return Ok();
      //try
      //{
      //   context.User.AddAsync(record);
      //   var saveChanges = await context.SaveChangesAsync();
      //   if (saveChanges <= 0) return BadRequest(new ApiResponse(400, MessageConstants.SaveFailed));
      //   return Ok();
      //}
      //catch (DbUpdateException ex)
      //{        
      //   return BadRequest(new ApiResponse(500, ex.InnerException.Message));
      //}
      //catch (Exception ex)
      //{
      //   return BadRequest(new ApiResponse(500, ex.Message));
      //}
   }
   #endregion CreateSecBUser

   #region ReadSecBUsers
   [Route(RouteConstant.ReadSecBUsers)]
   [HttpGet]
   [ProducesResponseType(typeof(BSecBUserDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(BSecBUserDto), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> ReadSecBUsers([FromQuery] SpecificationParams specParams)
   {
      //var countSpec = new BSecBUserSpecification(specParams);
      //var totalItems = await context.User.CountAsync(countSpec);

      var spec = new BSecBUserSpecification(specParams);
      IReadOnlyList<BSecBUser> users = await context.User.ListAsyncWithSpec(spec);
      if (users.Count() == 0) return NotFound(new ApiResponse(404, MessageConstants.NoRecordError));
      IReadOnlyList<BSecBUserDto> records = SecurityMappingProfile.UserEntitiesToDtos(users);

      //var users = GenericDataMapping.EntitiesToDtos<User, SecBUserDto>(records);
      //return Ok(new Pagination<SecBUserDto>(specParams.PageIndex, specParams.PageSize, totalItems, users));

      //IReadOnlyList<BSecBUser> users = await context.User.ListAsyncDesc(x => x.UserId);
      //if (users.Count() == 0) return NotFound(new ApiResponse(404, MessageConstants.NoRecordError));

      return Ok(records);
   }
   #endregion ReadSecBUsers

   #region ReadSecBUserByKey
   [Route(RouteConstant.ReadSecBUserByKey)]
   [HttpGet]
   [ProducesResponseType(typeof(BSecBUserDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> ReadSecBUserByKey(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new BSecBUserSpecification(key);

      BSecBUser user = await context.User.GetByKeyWithSpec(spec);
      if (user == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));
      BSecBUserDto record = SecurityMappingProfile.UserEntityToDto(user);
      return Ok(record);
   }
   #endregion ReadSecBUserByKey

   #region UpdateSecBUser
   [Route(RouteConstant.UpdateSecBUser)]
   [HttpPut]
   [ProducesResponseType(typeof(BSecBUserDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(BSecBUserDto), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> UpdateSecBUser(long key, BSecBUserDto model)
   {
      if (key == 0 || key != model.UserId) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));
      var spec = new BSecBUserSpecification(key);
      BSecBUser user = await context.User.GetByKeyWithSpec(spec);
      if (user == null) return BadRequest(new ApiResponse(400, MessageConstants.NoMatchFoundError));
      BSecBUser record = GenericDataMapping.ReplaceEntityWithDto<BSecBUser, BSecBUserDto>(user, model);

      context.User.UpdateEntity(record);
      var saveChanges = await context.SaveChangesAsync();
      if (saveChanges <= 0) return BadRequest(new ApiResponse(400, MessageConstants.SaveFailed));
      return Ok();

   }
   #endregion UpdateSecBUser

   #region DeleteSecBUser
   [Route(RouteConstant.DeleteSecBUser)]
   [HttpDelete]
   [ProducesResponseType(typeof(BSecBUserDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> DeleteSecBUser(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordDeleteError));
      var spec = new BSecBUserSpecification(key);
      // Check AddInclude for foreign key refference;
      BSecBUser record = await context.User.GetByKeyWithSpec(spec);

      //if (record.UserGroup.Count != 0 && record.BSecELinkUserGroupScreens.Count != 0 && record.BSecGLinkUserGroupScreenCommands.Count != 0)
      //   return BadRequest(new ApiResponse(400, MessageConstants.IfDeleteReffereceRecord));
      if (record == null) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordDeleteError));

      context.User.DeleteEntity(record);
      var saveChanges = await context.SaveChangesAsync();
      if (saveChanges <= 0) return BadRequest(new ApiResponse(400, MessageConstants.SaveFailed));

      return Ok();
   }
   #endregion DeleteSecBUser

}