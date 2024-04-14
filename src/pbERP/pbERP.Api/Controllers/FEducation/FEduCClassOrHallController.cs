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

public class FEduCClassOrHallController : BaseApiController
{
  private readonly IUnitOfWork context;

  public FEduCClassOrHallController(IUnitOfWork context)
  {
    this.context = context;
  }

  #region CreateEduCClassOrHall
  [Route(RouteConstant.CreateEduCClassOrHall)]
  [HttpPost]
  [ProducesResponseType(typeof(FEduCClassOrHallDto), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
  public async Task<IActionResult> CreateEduCClassOrHall(FEduCClassOrHallDto model)
  {
    if (model == null || model.HallSeatId != 0 || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));
    if (await context.ClassOrHall.IsExists(x => x.HallSeatNumber == model.HallSeatNumber)) return Conflict(new ApiResponse(409, MessageConstants.DuplicateError));

    model.HallSeatId = await context.ClassOrHall.GetNextId("HallSeatId");

    FEduCClassOrHall record = GenericDataMapping.DtoToEntity<FEduCClassOrHall, FEduCClassOrHallDto>(model);
    context.ClassOrHall.AddAsync(record);
    return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
  }
  #endregion CreateEduCClassOrHall

  #region ReadEduCClassOrHalls
  [Route(RouteConstant.ReadEduCClassOrHalls)]
  [HttpGet]
  [ProducesResponseType(typeof(FEduCClassOrHallDto), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> ReadEduCClassOrHalls([FromQuery] SpecificationParams specParams)
  {

    var spec = new FEduCClassOrHallSpecification(specParams);
    IReadOnlyList<FEduCClassOrHall> records = await context.ClassOrHall.ListAsyncWithSpec(spec);
    return (records.Count == 0) ? Ok(new List<FEduCClassOrHallDto>()) : Ok(EducationMappingProfile.ClassOrHallEntitiesToDtos(records));

    //IReadOnlyList<FEduCClassOrHall> recordsWithSpec = await context.ClassOrHall.ListAsync(predicate: x => x.HallSeatId, includeProperties: "ClassRoom");
    //return (records.Count == 0) ? BadRequest(new ApiResponse(404, MessageConstants.NoRecordError)) : Ok(EducationMappingProfile.ClassOrHallEntitiesToDtos(records));
  }
  #endregion ReadEduBBuildings

  #region ReadEduCClassOrHallByKey
  [Route(RouteConstant.ReadEduCClassOrHallByKey)]
  [HttpGet]
  [ProducesResponseType(typeof(FEduCClassOrHallDto), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> ReadEduCClassOrHall(long key)
  {
    if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
    var spec = new FEduCClassOrHallSpecification(key);
    FEduCClassOrHall record = await context.ClassOrHall.GetByKeyWithSpec(spec);
    return (record == null) ? NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError)) : Ok(EducationMappingProfile.ClassOrHallEntityToDto(record));
  }
  #endregion ReadEduBClassOrHallRoomByKey

  #region UpdateEduCClassOrHall
  [Route(RouteConstant.UpdateEduCClassOrHall)]
  [HttpPut]
  [ProducesResponseType(typeof(FEduCClassOrHallDto), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> UpdateEduCClassOrHall(long key, FEduCClassOrHallDto model)
  {
    if (key != model.HallSeatId) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));
    FEduCClassOrHall classOrHall = await context.ClassOrHall.GetFirstOrDefaultAsync(x => x.HallSeatId == model.HallSeatId);
    if (classOrHall == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));
    if (classOrHall.HallSeatNumber.ToString().Trim().ToLower() != model.HallSeatNumber.ToString().Trim().ToLower()) if (await context.ClassOrHall.IsExists(x => x.HallSeatNumber.ToString().Trim().ToLower() == model.HallSeatNumber.ToString().Trim().ToLower()) == true) return Conflict(new ApiResponse(409, MessageConstants.DuplicateError));

    FEduCClassOrHall record = GenericDataMapping.ReplaceEntityWithDto(classOrHall, model);
    context.ClassOrHall.UpdateEntity(record);
    return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
  }
  #endregion UpdateEduCClassOrHall

  #region DeleteEduCClassOrHall
  [Route(RouteConstant.DeleteEduCClassOrHall)]
  [HttpDelete]
  [ProducesResponseType(typeof(FEduCClassOrHallDto), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> DeleteEduCClassOrHall(long key)
  {
    if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
    var spec = new FEduCClassOrHallDelete(key);
    FEduCClassOrHall record = await context.ClassOrHall.GetByKeyWithSpec(spec);
    if (record == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));
    if (record.FEduDStudentAllocateHallSeats.Count != 0) return BadRequest(new ApiResponse(400, MessageConstants.IfDeleteReffereceRecord));
    context.ClassOrHall.DeleteEntity(record);
    return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.DeleteFailed)) : Ok();
  }
  #endregion DeleteEduCClassOrHall
}
