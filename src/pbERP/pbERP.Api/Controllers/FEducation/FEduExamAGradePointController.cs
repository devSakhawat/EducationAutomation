using Microsoft.AspNetCore.Mvc;
using pbERP.Api.Errors;
using pbERP.Domain.DTOs.FEducation;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts;
using pbERP.Infrastructure.DataMapping;
using pbERP.Infrastructure.Specifications;
using pbERP.Infrastructure.Specifications.Education;
using pbERP.Utilities.Constant;

namespace pbERP.Api.Controllers.FEducation;

public class FEduExamAGradePointController : BaseApiController
{
  private readonly IUnitOfWork context;

  public FEduExamAGradePointController(IUnitOfWork context)
  {
    this.context = context;
  }

  #region CreateFEduExamAGradePoint
  [Route(RouteConstant.CreateFEduExamAGradePoint)]
  [ProducesResponseType(typeof(FEduExamAGradePointDto), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
  public async Task<IActionResult> CrateFEduExamAGradePoint(FEduExamAGradePointDto model)
  {
    if (model == null || model.GradePointId != 0) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));

    model.GradePointId = await context.ExamAGradePoint.GetNextId("GradePointId");

    FEduExamAGradePoint record = GenericDataMapping.DtoToEntity<FEduExamAGradePoint, FEduExamAGradePointDto>(model);

    context.ExamAGradePoint.AddAsync(record);
    return await context.SaveChangesAsync() > 0 ? Ok() : BadRequest(new ApiResponse(400, MessageConstants.SaveFailed));
  }
  #endregion FEduExamAGradePoint

  #region ReadFEduExamAGradePoints
  public async Task<IActionResult> ReadFEduExamAGradePoints([FromQuery] SpecificationParams specParams)
  {
    var spec = new FEduExamAGradePointSpecification(specParams);
    IReadOnlyList<FEduExamAGradePoint> records = await context.ExamAGradePoint.ListAsyncWithSpec(spec);
  }
  #endregion ReadFEduExamAGradePoints
}
