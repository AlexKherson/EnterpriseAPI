using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthTestTokenController : ControllerBase
  {
    [HttpGet]
    public IActionResult Get()
    {
      if (HttpContext.User.Identity.IsAuthenticated)
        return Ok(HttpContext.User.Identity.Name);


      return Unauthorized();
    }
  }
}
