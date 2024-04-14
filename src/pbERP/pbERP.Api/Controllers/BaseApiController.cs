using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using pbERP.Utilities.Constant;

namespace pbERP.Api.Controllers
{
    [Route(RouteConstant.BaseRoute)]
    [ApiController]
    [EnableCors]
   public class BaseApiController : ControllerBase
    {
    }
}
