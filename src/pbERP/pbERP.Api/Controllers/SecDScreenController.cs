using AutoMapper;
using pbERP.Domain.Models.BSecurity;
using pbERP.Infrastructure.Constracts;

namespace pbERP.Api.Controllers
{
    public class SecDScreenController : BaseApiController
   {
      private readonly IGenericRepository<BSecDScreen> secDScreenRepo;
      private readonly IMapper mapper;

      public SecDScreenController(IGenericRepository<BSecDScreen> secDScreenRepo, IMapper mapper)
      {
         this.secDScreenRepo = secDScreenRepo;
         this.mapper = mapper;
      }

      //[Route(RouteConstant.ReadCompanyBaseModule)]
      //[HttpGet]
      //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
      //[ProducesResponseType(typeof(Pagination<SecDScreen>), StatusCodes.Status200OK)]
      //public async Task<IActionResult> ReadModuleBaseScreen(long moduleId)
      //{
      //   if (moduleId <= 0) return BadRequest(new ApiResponse(400));

      //   IReadOnlyList<SecDScreen> secDScreens = await secDScreenRepo.ListAsync(x => x.ModuleId == moduleId);

      //   if (secDScreens is null) return NotFound(new ApiResponse(404));




      //   //var companyLinkModules = await companyLinkModuleRepo.ListAsync();

      //   //if (companyLinkModules.IsNullOrEmpty()) return NotFound(new ApiResponse(404));

      //   //return Ok(companyLinkModules);
      //   return Ok();
      //}
   }
}
