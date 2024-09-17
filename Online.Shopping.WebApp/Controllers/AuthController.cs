using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Online.Shopping.WebApp.Models;
using Online.Shopping.WebApp.Models.AuthDtos;
using Online.Shopping.WebApp.Services.Contracts;
using Online.Shopping.WebApp.Utilities;

namespace Online.Shopping.WebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginDto loginDto = new();
            return View(loginDto);
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            ResponseDto result = await _authService.LoginAsync(loginDto);

            if(result != null && result.IsSuccess)
            {
                LoginResponseDto response = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(result.Result));
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("CustomError", result.Message);
                return View(loginDto);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem { Text = ServiceDependency.RoleAdmin, Value = ServiceDependency.RoleAdmin },
                new SelectListItem { Text = ServiceDependency.RoleCustomer, Value = ServiceDependency.RoleCustomer }
            };

            ViewBag.RoleList = roleList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            ResponseDto response = await _authService.RegisterAsync(model);

            ResponseDto assignRole;

            if(response != null && response.IsSuccess == true)
            {
                if(string.IsNullOrEmpty(model.Role))
                {
                    model.Role = ServiceDependency.RoleCustomer;
                }
                assignRole = await _authService.AssignRoleAsync(model);

                if(assignRole != null && assignRole.IsSuccess)
                {
                    TempData["success"] = "Registration is successful!";
                    return RedirectToAction(nameof(Login));
                }
            }
            else
            {
                TempData["error"] = response.Message;
            }

            var roleList = new List<SelectListItem>()
            {
                new SelectListItem { Text = ServiceDependency.RoleAdmin, Value = ServiceDependency.RoleAdmin },
                new SelectListItem { Text = ServiceDependency.RoleCustomer, Value = ServiceDependency.RoleCustomer }
            };

            ViewBag.RoleList = roleList;
            return View(model);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }
    }


}
