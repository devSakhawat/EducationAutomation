using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pbERP.Api.Errors;
using pbERP.Domain.DTOs.Menu;
using pbERP.Domain.Models.AGeneralConfig;
using pbERP.Domain.Models.BSecurity;
using pbERP.Infrastructure.Constracts;
using pbERP.Utilities.Constant;

namespace pbERP.Api.Controllers;

public class MenuController : BaseApiController
{
  private readonly IGenericRepository<AGenConfigJCompanyLinkModule> companyLinkModuleRepo;
  private readonly IGenericRepository<BSecDScreen> secDScreenRepo;
  private readonly IMapper mapper;

  private readonly IUnitOfWork _context;
  public MenuController(IUnitOfWork context, IGenericRepository<AGenConfigJCompanyLinkModule> companyLinkModuleRepo, IGenericRepository<BSecDScreen> secDScreenRepo, IMapper mapper)
  {
    _context = context;
    this.companyLinkModuleRepo = companyLinkModuleRepo;
    this.secDScreenRepo = secDScreenRepo;
    this.mapper = mapper;
  }

  [Route(RouteConstant.MainMenu)]
  [HttpGet]
  [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(MainMenuDto), StatusCodes.Status200OK)]
  public async Task<IActionResult> MainMenu([FromQuery] long companyId, long userId)
  {
    IReadOnlyList<MainMenuDto> mainMenus = await _context.MainMenu.GetMainMenus(companyId, userId);

    if (mainMenus.Count == 0)
      return NotFound(new ApiResponse(400));
    return Ok(mainMenus);
  }

  [Route(RouteConstant.SubMenu)]
  [HttpGet]
  [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(MainMenuDto), StatusCodes.Status200OK)]
  public async Task<IActionResult> SubMenus([FromQuery] long parentId)
  {
    IReadOnlyList<MainMenuDto> subMenus = await _context.MainMenu.GetSubManus(parentId);

    if (subMenus.Count == 0)
      return Ok(new List<MainMenuDto>());
    //return NotFound(new ApiResponse(400));
    return Ok(subMenus);
  }

  ///// <summary>
  ///// Get MainMenu(Module Name) form SecDScreen where parentId == 0. (parentId == 0 means module list)
  ///// </summary>
  //[Route(RouteConstant.MainMenu)]
  //[HttpGet]
  //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
  //[ProducesResponseType(typeof(MainMenuDto), StatusCodes.Status200OK)]
  //public async Task<IActionResult> MainMenu([FromQuery] long companyId)
  //{
  //   var spec = new MainMenuSpecification(companyId);
  //   IReadOnlyList<SoftConfigJCompanyLinkModule> companyLinkModules = await companyLinkModuleRepo.ListAsyncWithSpec(spec);
  //   IReadOnlyList<MainMenuDto> mainMenus = mapper.Map<IReadOnlyList<MainMenuDto>>(companyLinkModules);

  //   return Ok(mainMenus);
  //}


  ///// <summary>
  ///// Get SubMenu(Screen Name) form SecDScreen. Retreive data using moduleId.
  ///// </summary>
  //[Route(RouteConstant.SubMenu)]
  //[HttpGet]
  //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
  //[ProducesResponseType(typeof(SubMenuDto), StatusCodes.Status200OK)]
  //public async Task<IActionResult> SubMenu(long moduleId)
  //{
  //   if (moduleId <= 0) return BadRequest(new ApiResponse(400));

  //   IReadOnlyList<SecDScreen> secDScreens = await secDScreenRepo.ListAsync(x => x.ModuleId == moduleId);

  //   if (secDScreens is null) return NotFound(new ApiResponse(404));

  //   return Ok(secDScreens);
  //}
}
