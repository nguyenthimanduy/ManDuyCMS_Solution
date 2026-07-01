
/*
 * Ho va ten: Nguyen Thi Man Duy
 * Mssv: 2123110470
 * Version 2
 * Ngay thuc hien: 7/6/2026
 */
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using CMS.Data;
using System.Security.Claims;

namespace CMS.Backend.Controllers
{
    public class AccountController : Controller
    {

        private readonly ApplicationDbContext _context;


        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult CreateHash()
        {
            var hash = BCrypt.Net.BCrypt.HashPassword("123456");

            return Content(hash);
        }



        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }


            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }






        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(
            string username,
            string password,
            string? returnUrl = null)
        {


            if (string.IsNullOrEmpty(username) ||
               string.IsNullOrEmpty(password))
            {

                ViewData["Error"] =
                    "Vui lòng nhập đầy đủ thông tin";

                return View();

            }






            var user = await _context.Users
                .FirstOrDefaultAsync(x =>
                    x.Username == username);






            // ==============================
            // CHECK BCrypt
            // ==============================

            if (user == null ||
               !BCrypt.Net.BCrypt.Verify(
                    password,
                    user.PasswordHash))
            {

                ViewData["Error"] =
                    "Sai tài khoản hoặc mật khẩu";

                return View();

            }








            var claims = new List<Claim>
            {

                new Claim(
                    ClaimTypes.Name,
                    user.Username),


                new Claim(
                    ClaimTypes.Role,
                    user.Role),


                new Claim(
                    "FullName",
                    user.FullName ?? ""),


                new Claim(
                    "UserId",
                    user.Id.ToString())

            };








            var identity =
                new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);





            await HttpContext.SignInAsync(

                CookieAuthenticationDefaults.AuthenticationScheme,

                new ClaimsPrincipal(identity),

                new AuthenticationProperties
                {

                    IsPersistent = true,

                    ExpiresUtc =
                    DateTimeOffset.Now.AddHours(8)

                });






            return RedirectToAction(
                "Index",
                "Home");

        }









        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);


            return RedirectToAction(
                "Login",
                "Account");

        }






        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}