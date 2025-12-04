using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestauranteAPP_TP3.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        [HttpGet("/Account/Login")]
        public IActionResult Login(string returnUrl)
        {
            var dest = "/Identity/Account/Login";
            if (!string.IsNullOrEmpty(returnUrl))
                dest += "?ReturnUrl=" + WebUtility.UrlEncode(returnUrl);
            return Redirect(dest);
        }

        [HttpGet("/Account/Logout")]
        public IActionResult Logout(string returnUrl)
        {
            var dest = "/Identity/Account/Logout";
            if (!string.IsNullOrEmpty(returnUrl))
                dest += "?returnUrl=" + WebUtility.UrlEncode(returnUrl);
            return Redirect(dest);
        }

        [HttpGet("/Account/AccessDenied")]
        public IActionResult AccessDenied(string returnUrl)
        {
            var dest = "/Identity/Account/AccessDenied";
            if (!string.IsNullOrEmpty(returnUrl))
                dest += "?ReturnUrl=" + WebUtility.UrlEncode(returnUrl);
            return Redirect(dest);
        }
    }
}