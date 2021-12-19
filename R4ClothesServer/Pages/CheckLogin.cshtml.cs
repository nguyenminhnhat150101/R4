using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using R4ClothesServer.Helpers;
using R4ClothesServer.Models;
using R4ClothesServer.Models.ViewModels;

namespace R4ClothesServer.Pages
{
    [AllowAnonymous]
    public class CheckLoginModel : PageModel
    {
        [Inject]
        public IAPIHelper _apiHelper { get; set; }
        public ILocalStorageService _localStorageService { get; }
        public CheckLoginModel(ILocalStorageService localStorageService, IAPIHelper apiHelper)
        {
            _localStorageService = localStorageService;
            _apiHelper = apiHelper;
        }
        public async Task<IActionResult> OnGetAsync(string paramUsername, string paramPassword)
        {
            string returnUrl = Url.Content("~/");
            try
            {
                // Clear the existing external cookie
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch { }
            bool flagLogin = false;
            var loginQT = new Login() { User = paramUsername, Password = paramPassword };
            TokenModel tokenModel = new TokenModel();
            string res = await _apiHelper.PostRequestAsync("tokens", loginQT, null);
            if (res != "-1")
            {
                tokenModel = JsonConvert.DeserializeObject<TokenModel>(res);
            }
            if (tokenModel != null)
            {
                flagLogin = true;
            }
            if (flagLogin)// đăng nhập thành công
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, paramUsername),
                    new Claim(ClaimTypes.Role, "Admin"),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    RedirectUri = this.Request.Host.Value
                };

                try
                {
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                }
            }
            else
            {
            }
            return LocalRedirect(returnUrl);
        }


    }
}
