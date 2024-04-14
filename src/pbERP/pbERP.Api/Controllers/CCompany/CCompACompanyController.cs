using Microsoft.AspNetCore.Mvc;
using pbERP.Api.Errors;
using pbERP.Api.Helpers;
using pbERP.Domain.DTOs.CCompany;
using pbERP.Domain.DTOs.FEducation;
using pbERP.Domain.Models.CCompany;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts;
using pbERP.Infrastructure.DataMapping;
using pbERP.Infrastructure.Specifications;
using pbERP.Infrastructure.Specifications.Company;
using pbERP.Infrastructure.Specifications.Education;
using pbERP.Utilities.Constant;

namespace pbERP.Api.Controllers.CCompany
{
   public class CCompACompanyController : BaseApiController
   {
      private readonly IUnitOfWork context;

      public CCompACompanyController(IUnitOfWork context)
      {
         this.context = context;
      }

      #region CreateCCompACompany
      [Route(RouteConstant.CreateCCompACompany)]
      [HttpPost]
      [ProducesResponseType(typeof(CCompACompanyDto), StatusCodes.Status200OK)]
      [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
      public async Task<IActionResult> CreateCCompACompany(CCompACompanyDto model)
      {
         if (model == null || model.CompanyId != 0 || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));
         if (await context.ACompany.IsExists(x => x.CompanyCode == model.CompanyCode)) return Conflict(new ApiResponse(409, MessageConstants.DuplicateError));

         model.CompanyId = await context.ACompany.GetNextId("CompanyId");
         CCompACompany record = GenericDataMapping.DtoToEntity<CCompACompany, CCompACompanyDto>(model);
         context.ACompany.AddAsync(record);
         return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
      }
      #endregion CreateFEduFTransportCharge

      #region ReadCCompACompanys
      [Route(RouteConstant.ReadCCompACompanys)]
      [HttpGet]
      [ProducesResponseType(typeof(CCompACompanyDto), StatusCodes.Status200OK)]
      [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
      public async Task<IActionResult> ReadCCompACompanys([FromQuery] SpecificationParams specParams)
      {
         var spec = new CCompACompanySpecification(specParams);
         IReadOnlyList<CCompACompany> records = await context.ACompany.ListAsyncWithSpec(spec);
         return (records.Count <= 0) ? Ok(new List<CCompACompanyDto>()) : Ok(CompanyMappingProfile.CompanyEntitiesToDtos(records));
      }
      #endregion ReadFEduHStudentAllocateTransports

      #region ReadCCompACompanyByKey
      [Route(RouteConstant.ReadCCompACompanyByKey)]
      [HttpGet]
      [ProducesResponseType(typeof(CCompACompanyDto), StatusCodes.Status200OK)]
      [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
      public async Task<IActionResult> ReadCCompACompany(long key)
      {
         if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
         var spec = new CCompACompanySpecification(key);
         CCompACompany record = await context.ACompany.GetByKeyWithSpec(spec);
         return (record == null) ? NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError)) : Ok(CompanyMappingProfile.CompanyEntityToDto(record));
      }
      #endregion ReadCCompACompanyByKey

      #region UpdateCCompACompany
      [Route(RouteConstant.UpdateCCompACompany)]
      [HttpPut]
      [ProducesResponseType(typeof(CCompACompanyDto), StatusCodes.Status200OK)]
      [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
      public async Task<IActionResult> UpdateCCompACompany(long key, CCompACompanyDto model)
      {
         if (key != model.CompanyId) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));
         CCompACompany ACompany = await context.ACompany.GetFirstOrDefaultAsync(x => x.CompanyId == model.CompanyId);
         if (ACompany == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));

         CCompACompany record = GenericDataMapping.ReplaceEntityWithDto(ACompany, model);
         context.ACompany.UpdateEntity(record);
         return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.SaveFailed)) : Ok();
      }
      #endregion UpdateCCompACompany

      #region DeleteCCompACompany
      [Route(RouteConstant.DeleteCCompACompany)]
      [HttpDelete]
      [ProducesResponseType(typeof(CCompACompanyDto), StatusCodes.Status200OK)]
      [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
      public async Task<IActionResult> DeleteCCompACompany(long key)
      {
         if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
         var spec = new CCompACompanyDelete(key);
         CCompACompany record = await context.ACompany.GetByKeyWithSpec(spec);
         if (record == null) return BadRequest(new ApiResponse(404, MessageConstants.NoMatchFoundError));

         context.ACompany.DeleteEntity(record);
         return (await context.SaveChangesAsync() <= 0) ? BadRequest(new ApiResponse(400, MessageConstants.DeleteFailed)) : Ok();
      }
      #endregion DeleteCCompACompany
   }
}
