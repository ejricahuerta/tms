using Infrastrucure.Models;
using Infrastrucure.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Areas.Account.Models;

namespace Web.Areas.Account.Pages
{
    public class LoginModel : PageModel
    {
        private readonly TicketDbContext context;
        private readonly IAuthentication authentication;


        [BindProperty]
        public LoginViewModel Login { get; set; }

        public bool HasErrors { get; set; } = false;

        public LoginModel(TicketDbContext context, IAuthentication authentication)
        {
            this.context = context;
            this.authentication = authentication;
        }



        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                //var user = await context.Users.FirstOrDefaultAsync(p => p.Username.ToLower().Equals(Login.Username.ToLower()));
                //if (user != null)
                //{
                HasErrors = !(await authentication.ValidateUser(Login.Username, Login.Password));

                if (!HasErrors)
                {
                    var claims = new List<Claim>();

                    var userClaim = new Claim(ClaimTypes.Name, Login.Username);

                    var emailClaim = new Claim("Email", Login.Username);

                    claims.Add(userClaim);
                    claims.Add(emailClaim);

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(30),

                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    return Redirect("~/Tickets");
                }
                //}
            }
            HasErrors = true;
            return Page();
        }

        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("~/Account/Login");
        }
    }
}