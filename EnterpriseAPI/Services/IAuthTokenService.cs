using EnterpriseAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseAPI.Services
{
  public interface IAuthTokenService
  {
    UserModel TryGetUser(LoginModel model);
    string CreateToken(UserModel user);
  }
}
