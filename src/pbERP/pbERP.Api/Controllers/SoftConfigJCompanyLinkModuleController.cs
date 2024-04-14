using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pbERP.Api.Errors;
using pbERP.Domain.Models.AGeneralConfig;
using pbERP.Infrastructure.Constracts;
using pbERP.Infrastructure.Specifications;
using pbERP.Utilities.Constant;

namespace pbERP.Api.Controllers;
public class SoftConfigJCompanyLinkModuleController : BaseApiController
{
  private readonly IGenericRepository<AGenConfigJCompanyLinkModule> companyLinkModuleRepo;
  private readonly IMapper mapper;

  public SoftConfigJCompanyLinkModuleController(IGenericRepository<AGenConfigJCompanyLinkModule> companyListModuleRepo, IMapper mapper)
  {
    this.companyLinkModuleRepo = companyListModuleRepo;
    this.mapper = mapper;
  }

  [Route(RouteConstant.ReadCompanyBaseModule)]
  [HttpGet]
  [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
  //[ProducesResponseType(typeof(Pagination<SoftConfigJCompanyLinkModuleDto>), StatusCodes.Status200OK)]
  public async Task<IActionResult> ReadCompanyBaseModule([FromQuery] SpecificationParams specParams)
  {
    //var countSpec = new SoftConfigJCompanyLinkModuleWithFiltersForCountSpecification(specParams);
    var spec = new SoftConfigJCompanyLinkModuleSpecification(specParams);

    //var totalItems = await companyLinkModuleRepo.CountAsync(countSpec);
    var companyLinkModules = await companyLinkModuleRepo.ListAsyncWithSpec(spec);

    //var data = mapper.Map<IReadOnlyList<SoftConfigJCompanyLinkModuleDto>>(companyLinkModules);

    //return Ok(new Pagination<SoftConfigJCompanyLinkModuleDto>(specParams.PageIndex, specParams.PageSize, 0, data));


    //var companyLinkModules = await companyLinkModuleRepo.ListAsync();

    //if (companyLinkModules.IsNullOrEmpty()) return NotFound(new ApiResponse(404));

    return Ok(companyLinkModules);
  }


}
