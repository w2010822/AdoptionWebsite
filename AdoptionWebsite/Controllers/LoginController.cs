using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdoptionWebsite.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using System.Security.Principal;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace AdoptionWebsite.Controllers
{
    public class LoginController : Controller
    {
        public Animal_AdoptionContext _db = new Animal_AdoptionContext();

        private readonly IHttpContextAccessor _accessor;

        public LoginController(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public IActionResult Index()
        {


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(DbUser user)
        {
            IQueryable<DbUser> item = from m in _db.DbUser
                                      where m.UserName == user.UserName && m.UserPassword == user.UserPassword
                                      select m;
            DbUser userinfo = item.FirstOrDefault();
            if (userinfo != null)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, userinfo.UserName)
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await _accessor.HttpContext.SignInAsync(claimsPrincipal);

                return RedirectToAction("Index", "Animal");
            }

            ModelState.AddModelError("Error", "登入失敗");
            return View();
        }

        public async Task<IActionResult> SignOut()
        {
            await _accessor.HttpContext.SignOutAsync();

            return RedirectToAction("Index");
        }
    }
}
