//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using pbERP.Api.Errors;
//using pbERP.Api.Helpers;
//using pbERP.Domain.DTOs.Education;
//using pbERP.Domain.Models.FEducation;
//using pbERP.Infrastructure.Constracts;
//using pbERP.Infrastructure.Specifications;
//using pbERP.Utilities.Constant;

//namespace pbERP.Api.Controllers.Education
//{
//   public class EduBBuildingController : BaseApiController
//   {
//      private readonly IMapper mapper;
//      private readonly IUnitOfWork context;

//      public EduBBuildingController(IUnitOfWork context)
//      {
//         this.context = context;
//      }

//      #region Read_EduABuilding
//      [Route(RouteConstant.ReadEduBBuilding)]
//      [HttpGet]
//      [ProducesResponseType(typeof(Pagination<EduBBuildingToReturnDto>), StatusCodes.Status200OK)]
//      [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
//      public async Task<IActionResult> ReadEduABuilding([FromQuery] SpecificationParams eduABuildingParams)
//      {
//         var spec = new EduBBuildingSpecification(eduABuildingParams);
//         var countSpec = new EduABuildingsWithFiltersForCountSpecification(eduABuildingParams);

//         var eduBBuildings = await context.EduBBuilding.ListAsyncWithSpec(spec);
//         var totalItems = await context.EduBBuilding.CountAsync(countSpec);
//         if (eduBBuildings.Count() == 0) return NotFound(new ApiResponse(404));

//         var data = mapper.Map<IReadOnlyList<EduBBuildingDto>>(eduBBuildings);

//         return Ok(new Pagination<EduBBuildingDto>(eduABuildingParams.PageIndex,
//             eduABuildingParams.PageSize, totalItems, data));
//      }
//      #endregion

//      #region Read_EduABuildingByKey
//      [Route(RouteConstant.ReadEduBBuildingByKey)]
//      [HttpGet]
//      [ProducesResponseType(typeof(EduBBuildingDto), StatusCodes.Status200OK)]
//      [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
//      public async Task<IActionResult> ReadEduABuildingByKey(int key)
//      {
//         var spec = new EduBBuildingSpecification(key);

//         FEduBBuilding eduABuilding = await context.EduBBuilding.GetByKeyWithSpec(spec);

//         if (eduABuilding is null) return NotFound(new ApiResponse(404));

//         EduBBuildingDto eduABuildingInfoToReturnDto = mapper.Map<FEduBBuilding, EduBBuildingDto>(eduABuilding);


//         return Ok(eduABuildingInfoToReturnDto);
//      }
//      #endregion

//      #region ReadEduABuildingWithoutParams
//      [Route(RouteConstant.ReadEduBBuildingWithoutParams)]
//      [HttpGet]
//      [ProducesResponseType(typeof(Pagination<EduBBuildingDto>), StatusCodes.Status200OK)]
//      [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
//      public async Task<IActionResult> ReadEduABuildingWithoutParams([FromQuery] SpecificationParams eduABuildingParams)
//      {
//         var spec = new EduBBuildingSpecification(eduABuildingParams);
//         var countSpec = new EduABuildingsWithFiltersForCountSpecification(eduABuildingParams);
//         IReadOnlyList<FEduBBuilding> eduABuildings = await context.EduBBuilding.ListAsyncWithSpec(spec);
//         //var data = mapper.Map<IReadOnlyList<EduBBuilding>>(eduABuildings);
//         //return Ok(data);
//         var totalItems = await context.EduBBuilding.CountAsync(countSpec);

//         //var data = mapper.Map<IReadOnlyList<EduABuildingInfoToReturnDto>>(products);

//         IReadOnlyList<EduBBuildingDto> data = eduABuildings.Select(x => new EduBBuildingDto()
//         {
//            BuildingId = x.BuildingId,
//            BuildingNameEnglish = x.BuildingName,
//            BuildingNameLocal = x.BuildingNameLocal,
//            CompanyId = x.CompanyId,
//            CompanyName = (x.Company.CompanyName != null) ? x.Company.CompanyName : null,
//            UsesType = x.UsesType
//         }).ToList();

//         return Ok(new Pagination<EduBBuildingDto>(eduABuildingParams.PageIndex,
//             eduABuildingParams.PageSize, totalItems, data));
//      }
//      #endregion
//   }
//}
