using DEMO.DAL.Entity;
using DEMO.PL.Helper;
using DEMO.PL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using System.ComponentModel.DataAnnotations;

namespace DEMO.PL.Controllers
{
    public class Account : Controller
    {
        private readonly UserManager<AplicationUser> _userManager;
		private readonly SignInManager<AplicationUser> _signInManager;
		private readonly ILogger<Account> _logger;

        public Account(
            UserManager<AplicationUser> userManager,
            SignInManager<AplicationUser> signInManager
            ,ILogger<Account> logger
            )
        {
            _userManager = userManager;
			_signInManager = signInManager;
			_logger = logger;
        }

        public IActionResult SignUp()
        {
            return View(new SignUpViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel input)
        {
            if(ModelState.IsValid)
            
            {
                var user = new AplicationUser
                {
                    Email = input.Email,
                    UserName = input.Email.Split("@")[0],
                    IsActive=true
                };

                var res = await _userManager.CreateAsync(user, input.Password);
                if(res.Succeeded)
                    return RedirectToAction("Login");

                foreach (var error in res.Errors)
                {
                    _logger.LogError(error.Description);
                    ModelState.AddModelError("", error.Description);  
                }

            }  
            return View(input);

        }

		public IActionResult Login()
		{
			return View(new SignInViewModel());
		}

        [HttpPost]
		public async Task<IActionResult> Login(SignInViewModel input)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(input.Email);
				if (user is null)
				{
					ModelState.AddModelError("", "User not found in the system");
				}
				else
				{
					var result1 = await _signInManager.PasswordSignInAsync(user, input.Password, input.RememderME, false);
					if (result1.Succeeded)
					{
						return RedirectToAction("Index", "Home");
					}
					else
					{
						ModelState.AddModelError("", "Invalid login attempt");
					}
				}
			}
			return View(input);
		}


		public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult ForgetPasword()
        {
            return View(new ForgetPasswordViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPasword(ForgetPasswordViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(input.Email);

                if (user is not null)
                {


                    //Token
                   var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                  
                    
                    
                    //Creat URL
                  var url =  Url.Action("ResetPassword", "AccountController1", new {Email= input.Email , token = token},Request.Scheme);

                    //creat Email
                    var email = new Email
                    {
                        Title = "reset your passowrd",
                        To = input.Email,
                        Body = url

                    };      
                        //send Email

                    EmailSiting.SendEmail(email);

                    // redirect to action
                    return RedirectToAction(nameof(ChekPYpurIbox));

                }
                ModelState.AddModelError(string.Empty, "Invalid Email");


            }
            return View(nameof(ForgetPasword), input);        
        }


        public IActionResult ChekPYpurIbox()
        {
            return View();
        }

        [HttpGet]

        public IActionResult ResetPassword(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;  

            return View();  
        }

        [HttpPost]
        public async Task< IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
           if(ModelState.IsValid)
            {
               var email = TempData["email"] as string;
               var token = TempData["token"] as string;

              var user = await  _userManager.FindByEmailAsync(email);
                if (user !=  null) 
                {
                   var result= await _userManager.ResetPasswordAsync(user, token,model.NewPassword);
                    if(result.Succeeded)
                        return RedirectToAction(nameof(Login));

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                ModelState.AddModelError(string.Empty, "invalid Reset Password");
            }
           return View(model);
        }


        public IActionResult AccessDenied()
        {
                return View();
        }
    }
}

