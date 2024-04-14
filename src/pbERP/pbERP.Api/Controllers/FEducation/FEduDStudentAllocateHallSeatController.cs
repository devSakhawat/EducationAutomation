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

public class FEduDStudentAllocateHallSeatController : BaseApiController
{
   private readonly IUnitOfWork context;

   public FEduDStudentAllocateHallSeatController(IUnitOfWork context)
   {
      this.context = context;
   }

   #region CreateFEduDStudentAllocateHallSeat
   [Route(RouteConstant.CreateFEduDStudentAllocateHallSeat)]
   [HttpPost]
   [ProducesResponseType(typeof(FEduDStudentAllocateHallSeatDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> CreateFEduDStudentAllocateHallSeat(FEduDStudentAllocateHallSeatDto model)
   {
      if (model == null || model.AllocateStudentHallSeatId != 0 || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));
      if (await context.StudentSeat.IsExists(x => x.StudentId == model.StudentId)) return Conflict(new ApiResponse(409, MessageConstants.DuplicateError));
      if (await context.StudentSeat.IsExists(x => x.HallSeatId == model.HallSeatId)) return Conflict(new ApiResponse(409, MessageConstants.DuplicateError));

      model.AllocateStudentHallSeatId = await context.StudentSeat.GetNextId("AllocateStudentHallSeatId");
      FEduDStudentAllocateHallSeat record = GenericDataMapping.DtoToEntity<FEduDStudentAllocateHallSeat, FEduDStudentAllocateHallSeatDto>(model);
      context.StudentSeat.AddAsync(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion CreateFEduDStudentAllocateHallSeat

   #region ReadFEduDStudentAllocateHallSeats
   [Route(RouteConstant.ReadFEduDStudentAllocateHallSeats)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduDStudentAllocateHallSeatDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduFTransportCharges([FromQuery] SpecificationParams specParams)
   {
      var spec = new FEduDStudentAllocateHallSeatSpecification(specParams);
      IReadOnlyList<FEduDStudentAllocateHallSeat> records = await context.StudentSeat.ListAsyncWithSpec(spec);
      return (records.Count == 0) ? Ok(new List<FEduDStudentAllocateHallSeatDto>()) : Ok(EducationMappingProfile.StudentSeatEntitiesToDtos(records));
   }
   #endregion ReadFEduFTransportCharges

   #region ReadFEduDStudentAllocateHallSeatByKey
   [Route(RouteConstant.ReadFEduDStudentAllocateHallSeatByKey)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduDStudentAllocateHallSeatDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduDStudentAllocateHallSeat(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new FEduDStudentAllocateHallSeatSpecification(key);
      FEduDStudentAllocateHallSeat record = await context.StudentSeat.GetByKeyWithSpec(spec);
      return (record == null) ? NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError)) : Ok(EducationMappingProfile.StudentSeatEntityToDto(record));
   }
   #endregion ReadFEduDStudentAllocateHallSeatByKey

   #region UpdateFEduDStudentAllocateHallSeat
   [Route(RouteConstant.UpdateFEduDStudentAllocateHallSeat)]
   [HttpPut]
   [ProducesResponseType(typeof(FEduDStudentAllocateHallSeatDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> UpdateFEduDStudentAllocateHallSeat(long key, FEduDStudentAllocateHallSeatDto model)
   {
      if (key != model.AllocateStudentHallSeatId) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));
      FEduDStudentAllocateHallSeat studentSeat = await context.StudentSeat.GetFirstOrDefaultAsync(x => x.AllocateStudentHallSeatId == model.AllocateStudentHallSeatId);
      if (await context.StudentSeat.IsExists(x => x.StudentId == model.StudentId)) return Conflict(new ApiResponse(409, MessageConstants.DuplicateError));
      if (await context.StudentSeat.IsExists(x => x.HallSeatId == model.HallSeatId)) return Conflict(new ApiResponse(409, MessageConstants.DuplicateError));

      FEduDStudentAllocateHallSeat record = GenericDataMapping.ReplaceEntityWithDto(studentSeat, model);
      context.StudentSeat.UpdateEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
   }
   #endregion UpdateEduCClassOrHall

   #region DeleteFEduDStudentAllocateHallSeat
   [Route(RouteConstant.DeleteFEduDStudentAllocateHallSeat)]
   [HttpDelete]
   [ProducesResponseType(typeof(FEduDStudentAllocateHallSeatDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> DeleteFEduDStudentAllocateHallSeat(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      var spec = new FEduDStudentAllocateHallSeatDelete(key);
      FEduDStudentAllocateHallSeat record = await context.StudentSeat.GetByKeyWithSpec(spec);
      if (record == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));

      context.StudentSeat.DeleteEntity(record);
      return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.DeleteFailed)) : Ok();
   }
   #endregion DeleteFEduDStudentAllocateHallSeat
}
