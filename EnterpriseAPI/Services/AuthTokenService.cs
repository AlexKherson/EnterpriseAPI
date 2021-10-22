using EnterpriseAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseAPI.Services
{

  enum Roles
  {
    Admin,
    ProductManager
  }

  public class AuthTokenService : IAuthTokenService
  {
    private readonly IConfiguration _configuration;

    public AuthTokenService(IConfiguration configuration)
    {
      _configuration = configuration;
    }
    public UserModel TryGetUser(LoginModel model)
    {
      if (model.Username == "admin" && model.Password == "admin")
        return new UserModel
        {
          Id=1,
          Username="Admin_Admin"
        };
      else return null;        
    }

    public string CreateToken(UserModel user)
    {
      var claims= CreateClaims(user);
      
      return GetJWT(claims,_configuration["SignignKey"]);
    }

    private string GetJWT(List<Claim> claims, string key)
    {
      var now = DateTime.Now;

      var jwt = new JwtSecurityToken
      (
          notBefore: now,
          expires: now.AddMinutes(1),
          claims: claims,
          signingCredentials: new SigningCredentials(
              new SymmetricSecurityKey(
                          Encoding.UTF8.GetBytes(key)),
                          SecurityAlgorithms.HmacSha256)

      );
      var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
      return encodedJwt;
    }

    private List<Claim> CreateClaims(UserModel user)
    {
      return new List<Claim>
      {
        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
        new Claim(ClaimTypes.Name,user.Username),
        new Claim("roles",nameof(Roles.Admin)),
        new Claim("roles",nameof(Roles.ProductManager)),

      };
    }
  }
}
