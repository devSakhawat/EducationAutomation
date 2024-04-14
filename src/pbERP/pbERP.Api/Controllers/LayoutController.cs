using AutoMapper;
using pbERP.Domain.Models.AGeneralConfig;
using pbERP.Domain.Models.BSecurity;
using pbERP.Infrastructure.Constracts;

namespace pbERP.Api.Controllers
{
  public class LayoutController : BaseApiController
  {
    private readonly IGenericRepository<AGenConfigJCompanyLinkModule> companyLinkModuleRepo;
    private readonly IGenericRepository<BSecDScreen> secDScreenRepo;
    private readonly IMapper mapper;

    public LayoutController(IGenericRepository<AGenConfigJCompanyLinkModule> companyLinkModuleRepo, IGenericRepository<BSecDScreen> secDScreenRepo, IMapper mapper)
    {
      this.companyLinkModuleRepo = companyLinkModuleRepo;
      this.secDScreenRepo = secDScreenRepo;
      this.mapper = mapper;
    }

    ///// <summary>
    ///// Module list from SoftConfigJCompanyLinkModule entity will be main menu.
    ///// </summary>
    ///// <param name="specParams"></param>
    ///// <returns></returns>
    //[Route(RouteConstant.MainMenu)]
    //[HttpGet]
    //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    //[ProducesResponseType(typeof(Pagination<SoftConfigJCompanyLinkModuleDto>), StatusCodes.Status200OK)]
    //public async Task<IActionResult> MainMenu([FromQuery] SpecificationParams specParams)
    //{
    //  var countSpec = new SoftConfigJCompanyLinkModuleWithFiltersForCountSpecification(specParams);
    //  var spec = new SoftConfigJCompanyLinkModuleSpecification(specParams);

    //  var totalItems = await companyLinkModuleRepo.CountAsync(countSpec);
    //  var companyLinkModules = await companyLinkModuleRepo.ListAsyncWithSpec(spec);

    //  var data = mapper.Map<IReadOnlyList<SoftConfigJCompanyLinkModuleDto>>(companyLinkModules);

    //  return Ok(new Pagination<SoftConfigJCompanyLinkModuleDto>(specParams.PageIndex,
    //      specParams.PageSize, totalItems, data));
    //}

    ///// <summary>
    ///// Module List (which has access permission for the specific company) will be SubMenu
    ///// </summary>
    ///// <returns></returns>
    //public async Task<IActionResult> NestedMenu(long moduleId)
    //{
    //  var secDScreens = secDScreenRepo.ListAsync(sds => sds.ModuleId == moduleId);
    //  return Ok();
    //}
  }
}
