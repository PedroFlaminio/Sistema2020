using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sistema2020.Models;
using Sistema2020.Models.ViewModels;

namespace Sistema2020.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<Usuario> _userManager;
        private SignInManager<Usuario> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        public HomeController(
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public  IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var user = new Usuario
            {
                UserName = model.UserName
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var roleAdm = await _roleManager.FindByNameAsync("Administrador"); 
            if (roleAdm == null)
            {
                await _roleManager.CreateAsync(new IdentityRole {  Name = "Administrador" });
                roleAdm = await _roleManager.FindByNameAsync("Administrador");
            }
            var usuManutencao = await _userManager.FindByNameAsync("MANUTENCAO");

            if (usuManutencao == null)
            {
                usuManutencao = new Usuario { UserName = "MANUTENCAO" };
                var result = await _userManager.CreateAsync(usuManutencao, "elerato");
                if (result.Succeeded)
                {
                    usuManutencao = await _userManager.FindByNameAsync("MANUTENCAO");
                    await _userManager.AddToRoleAsync(usuManutencao, "Administrador");
                }
            }
            var usuPadrao = await _userManager.FindByNameAsync("PADRAO");
            if (usuPadrao == null)
            {
                usuPadrao = new Usuario { UserName = "PADRAO" };
                var result = await _userManager.CreateAsync(usuPadrao, "teste123");
                if (result.Succeeded)
                {
                    usuPadrao = await _userManager.FindByNameAsync("PADRAO");
                    await _userManager.AddToRoleAsync(usuPadrao, "Administrador");
                }
            }

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (signInResult.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}