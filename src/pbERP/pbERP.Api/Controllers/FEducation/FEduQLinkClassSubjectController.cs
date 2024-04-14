using Microsoft.AspNetCore.Mvc;
using pbERP.Api.Errors;
using pbERP.Api.Helpers;
using pbERP.Domain.DTOs.FEducation;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts;
using pbERP.Infrastructure.Specifications;
using pbERP.Infrastructure.Specifications.Education;
using pbERP.Utilities.Constant;
using System.Reflection.Metadata.Ecma335;

namespace pbERP.Api.Controllers.FEducation;

public class FEduQLinkClassSubjectController : BaseApiController
{
   private readonly IUnitOfWork context;

   public FEduQLinkClassSubjectController(IUnitOfWork context)
   {
      this.context = context;
   }

   #region CreateFEduQLinkClassSubject
   [Route(RouteConstant.CreateFEduQLinkClassSubject)]
   [HttpPost]
   [ProducesResponseType(typeof(FEduQLinkClassSubjectDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> CreateFEduQLinkClassSubject(List<FEduQLinkClassSubjectDto> models)
   {
      if (models.Count == 0 || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));

      if(models.Any(x => x.ClassId != null && x.ClassGroupId != null && x.ClassSubjectId != null))
      {
         int results = await context.LinkClassSubject.BulkInsertLinkClassSubject(models);
         return (results != 200) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
      }

      return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));
   }
   #endregion CreateFEduQLinkClassSubject

   #region ReadFEduQLinkClassSubject
   [Route(RouteConstant.ReadFEduQLinkClassSubjects)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduQLinkClassSubjectDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> ReadFEduQLinkClassSubject([FromQuery] SpecificationParams specParams)
   {
      var spec = new FEduQLinkClassSubjectSpecification(specParams);
      IReadOnlyList<FEduQLinkClassSubject> records = await context.LinkClassSubject.ListAsyncWithSpec(spec);
      return (records.Count <= 0) ? Ok(new List<FEduQLinkClassSubject>()) : Ok(EducationMappingProfile.LinkClassShiftsToDtos(records));
   }
   #endregion ReadFEduQLinkClassSubject
}
