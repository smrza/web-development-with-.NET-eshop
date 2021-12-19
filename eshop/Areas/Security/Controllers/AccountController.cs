using eshop.Controllers;
using eshop.Models;
using eshop.Models.ApplicationServices;
using eshop.Models.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Areas.Security.Controllers
{
    [Area("Security")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        ISecurityApplicationService iSecure;
        readonly ILogger<AccountController> logger;

        public AccountController(ISecurityApplicationService iSecure, ILogger<AccountController> logger)
        {
            this.iSecure = iSecure;
            this.logger = logger;
        }

        public IActionResult Login()
        {
            this.logger.LogInformation("Login was called.");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            this.logger.LogInformation("Login was called.");

            vm.LoginFailed = false;
            if (ModelState.IsValid)
            {
                bool isLogged = await iSecure.Login(vm);
                if (isLogged)
                {
                    return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", String.Empty), new { area = "" });
                }
                else
                {
                    this.logger.LogWarning("Login failed.");
                    vm.LoginFailed = true;
                }
            }
            return View(vm);
        }

        public IActionResult Logout()
        {
            this.logger.LogInformation("Logout was called.");

            iSecure.Logout();
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Register()
        {
            this.logger.LogInformation("Register was called.");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            this.logger.LogInformation("Register was called.");

            vm.ErrorsDuringRegister = null;
            if (ModelState.IsValid)
            {
                this.logger.LogError("Errors during registration.");
                vm.ErrorsDuringRegister = await iSecure.Register(vm, Models.Identity.Roles.Customer);

                if(vm.ErrorsDuringRegister == null)
                {
                    var lVM = new LoginViewModel()
                    {
                        Username = vm.Username,
                        Password = vm.Password,
                        RememberMe = true,
                        LoginFailed = false
                    };

                    return await Login(lVM);
                }
            }
            return View(vm);
        }
    }
}
