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

public class FEduBClassOrHallRoomController : BaseApiController
{
  private readonly IUnitOfWork context;

  public FEduBClassOrHallRoomController(IUnitOfWork context)
  {
    this.context = context;
  }
  #region CreateEduBClassOrHallRoom
  [Route(RouteConstant.CreateEduBClassOrHallRoom)]
  [HttpPost]
  [ProducesResponseType(typeof(FEduBClassOrHallRoomDto), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
  public async Task<IActionResult> CreateEduBClassOrHallRoom(FEduBClassOrHallRoomDto model)
  {
    if (model == null || model.ClassRoomId != 0 || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));
    if (await context.ClassOrHallRoom.IsExists(x => x.ClassRoomName == model.ClassRoomName)) return Conflict(new ApiResponse(409, MessageConstants.DuplicateError));

    model.ClassRoomId = await context.ClassOrHallRoom.GetNextId("ClassRoomId");

    FEduBClassOrHallRoom record = GenericDataMapping.DtoToEntity<FEduBClassOrHallRoom, FEduBClassOrHallRoomDto>(model);
    context.ClassOrHallRoom.AddAsync(record);
    return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
  }
  #endregion CreateEduBClassOrHallRoom

  #region ReadEduBClassOrHallRooms
  [Route(RouteConstant.ReadEduBClassOrHallRooms)]
  [HttpGet]
  [ProducesResponseType(typeof(FEduBClassOrHallRoomDto), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> ReadEduBClassOrHallRooms([FromQuery] SpecificationParams specParams)
  {
    var spec = new FEduBClassOrHallRoomSpecification(specParams);
    IReadOnlyList<FEduBClassOrHallRoom> records = await context.ClassOrHallRoom.ListAsyncWithSpec(spec);
    return (records.Count == 0) ? Ok(new List<FEduBClassOrHallRoomDto>()) : Ok(EducationMappingProfile.ClassOrHallRoomEntitiesToDtos(records));
  }
  #endregion ReadEduBBuildings

  #region ReadEduBClassOrHallRoomByKey
  [Route(RouteConstant.ReadEduBClassOrHallRoomByKey)]
  [HttpGet]
  [ProducesResponseType(typeof(FEduBClassOrHallRoomDto), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> ReadEduBClassOrHallRoomByKey(long key)
  {
    if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
    var spec = new FEduBClassOrHallRoomSpecification(key);
    FEduBClassOrHallRoom record = await context.ClassOrHallRoom.GetByKeyWithSpec(spec);
    return (record == null) ? NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError))
       : Ok(EducationMappingProfile.ClassOrHallRoomEntityToDto(record));
  }
  #endregion ReadEduBClassOrHallRoomByKey

  #region UpdateEduBClassOrHallRoom
  [Route(RouteConstant.UpdateEduBClassOrHallRoom)]
  [HttpPut]
  [ProducesResponseType(typeof(FEduBClassOrHallRoomDto), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> UpdateEduBClassOrHallRoom(long key, FEduBClassOrHallRoomDto model)
  {
    if (key != model.ClassRoomId) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));
    FEduBClassOrHallRoom classOrHallRoom = await context.ClassOrHallRoom.GetFirstOrDefaultAsync(x => x.ClassRoomId == model.ClassRoomId);
    if (classOrHallRoom == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));
    if (classOrHallRoom.ClassRoomName.Trim().ToLower() != model.ClassRoomName.Trim().ToLower()) if (await context.ClassOrHallRoom.IsExists(x => x.ClassRoomName.Trim().ToLower() == model.ClassRoomName.Trim().ToLower()) == true) return Conflict(new ApiResponse(409, MessageConstants.DuplicateError));

    FEduBClassOrHallRoom record = GenericDataMapping.ReplaceEntityWithDto(classOrHallRoom, model);
    context.ClassOrHallRoom.UpdateEntity(record);
    return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
  }
  #endregion UpdateEduBClassOrHallRoom

  #region DeleteEduBClassOrHallRoom
  [Route(RouteConstant.DeleteEduBClassOrHallRoom)]
  [HttpDelete]
  [ProducesResponseType(typeof(FEduBClassOrHallRoom), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> DeleteEduBClassOrHallRoom(long key)
  {
    if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
    var spec = new FEduBClassOrHallRoomDelete(key);
    FEduBClassOrHallRoom record = await context.ClassOrHallRoom.GetByKeyWithSpec(spec);
    if (record == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));
    if (record.FEduCClassOrHalls.Count != 0 || record.FEduDStudentAllocateHallSeats.Count != 0) return BadRequest(new ApiResponse(400, MessageConstants.IfDeleteReffereceRecord));
    context.ClassOrHallRoom.DeleteEntity(record);
    return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.DeleteFailed)) : Ok();
  }
  #endregion DeleteEduBBuilding
}
