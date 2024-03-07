using BlogApp.Entity.DTOs.Users;
using BlogApp.Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class AuthController : Controller
{
    private readonly UserManager<AppUser> _usermanager;
    private readonly SignInManager<AppUser> _signInManager;

    public AuthController(UserManager<AppUser> usermanager, SignInManager<AppUser> signInManager)
    {
        _usermanager = usermanager;
        _signInManager = signInManager;
    }
    
    // GET
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(UserLoginDto userLoginDto)
    {
        if (ModelState.IsValid)
        {
            var user = await _usermanager.FindByEmailAsync(userLoginDto.Email);
            if (user != null)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, userLoginDto.RememberMe,
                        false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index","Home", new {Area ="Admin"});
                }
                else
                {
                    ModelState.AddModelError("", "Eposta adresiniz veya şifreniz yanlıştır.");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("","Üyelik kaydınız bulunmamaktadır.");
                return View();
            }
        }
        else
        {
            return View();
        }
        
    }
            [Authorize]
            [HttpGet]
            public async Task<IActionResult> Logout()
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index","Home", new {Area =""});
            }
     
}