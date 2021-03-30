using CoreSimpam.Model;
using CoreSimpam.Model.Data;
using CoreSimpam.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.Repo
{
    public interface ILoginRepo
    {
        Task<bool> Login(LoginViewModel model);
        Task<bool> Logout();
    }
    public class LoginRepo : ILoginRepo
    {
        private readonly HttpContext _httpContext;
        private readonly IUserRepo _userRepo;
        private readonly ILogger logger;

        public LoginRepo(IHttpContextAccessor HttpContextAccessor, IUserRepo userRepo, ILogger<LoginRepo> logger)
        {
            _httpContext = HttpContextAccessor.HttpContext;
            _userRepo = userRepo;
            this.logger = logger;
        }

        public async Task<bool> Login(LoginViewModel model)
        {
            try
            {
                Metadata<UserViewModel> data = await _userRepo.Validate(model.username, model.password);
                var user = data.data;
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.RoleName),
                    new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString())
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principcal = new ClaimsPrincipal(identity);
                await _httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principcal);
                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<bool> Logout()
        {
            try
            {
                await _httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return true;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return false;
            }
        }
    }
}
