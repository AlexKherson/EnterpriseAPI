using EnterpriseAPI.Models;
using EnterpriseAPI.Services;
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
  public class AuthTokenController : ControllerBase
  {
    private readonly IAuthTokenService _authTokenService;

    public AuthTokenController(IAuthTokenService authTokenService)
    {
      _authTokenService = authTokenService;
    }

    [HttpPost]
    public IActionResult Login(LoginModel model)
    {
      var user = _authTokenService.TryGetUser(model);
      if (user is null) return Unauthorized();
      string encodedJWT = _authTokenService.CreateToken(user);   

      return Ok(new { Token=encodedJWT });
    }
  }
}
